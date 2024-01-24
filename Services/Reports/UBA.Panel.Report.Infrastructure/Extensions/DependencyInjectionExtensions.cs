using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UBA.Panel.Report.Domain.Interfaces;
using UBA.Panel.Report.Infrastructure.Context;
using UBA.Panel.Report.Infrastructure.Factories;
using UBA.Panel.Report.Infrastructure.Interfaces;
using UBA.Panel.Report.Infrastructure.Repositories;
using UBA.Panel.Report.Infrastructure.Services;

namespace UBA.Panel.Report.Infrastructure.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDbContext(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContext<ReportsDbContext>(options =>
            options.UseNpgsql(configuration["Database:ConnectionString"]));

        serviceCollection.AddScoped<IReportsRepository, ReportsRepository>();

        return serviceCollection;
    }

    public static IServiceCollection AddMediatR(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining(typeof(DependencyInjectionExtensions)));
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection
            .AddTransient<IReportItemDtoFactory, ReportItemDtoFactory>()
            .AddScoped<IReportExporterFactory, ReportExporterFactory>()
            .AddScoped(_ => 
                new BlobContainerClient(
                    configuration["FileStore:ConnectionString"],
                    configuration["FileStore:ContainerName"]))
            .AddScoped<IFileUploaderService, FileUploaderService>();
    }

    public static IServiceCollection AddFileProcessorServices(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        return serviceCollection
            .AddScoped<IFileProcessorService, FileProcessorService>((services) =>
            {
                var targetBlobContainerClient = new BlobContainerClient(
                    configuration["FileStore:ConnectionString"],
                    configuration["FileStore:TargetContainerName"]);
                targetBlobContainerClient.CreateIfNotExists();

                return new FileProcessorService(
                    new BlobContainerClient(
                        configuration["FileStore:ConnectionString"],
                        configuration["FileStore:ContainerName"]),
                    targetBlobContainerClient,
                    services.GetRequiredService<IReportsRepository>(),
                    services.GetRequiredService<ILogger<FileProcessorService>>(),
                    services.GetRequiredService<IVinChecksumCalculator>());
            })
            .AddTransient<IVinChecksumCalculator, VinChecksumCalculator>();
    }
}