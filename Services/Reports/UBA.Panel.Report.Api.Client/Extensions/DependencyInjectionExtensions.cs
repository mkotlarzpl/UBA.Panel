using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UBA.Panel.Report.Api.Client.Config;

namespace UBA.Panel.Report.Api.Client.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddReportsApiClient(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddSingleton<ReportApiClientConfig>((_) => new ReportApiClientConfig()
        {
            Endpoint = configuration["ReportsApi:Endpoint"]
        });
        return serviceCollection
            .AddScoped<IReportApiClient, ReportApiClient>();
    }
}