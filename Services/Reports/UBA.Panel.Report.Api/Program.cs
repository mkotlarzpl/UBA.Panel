using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using UBA.Panel.Report.Infrastructure.Context;
using UBA.Panel.Report.Infrastructure.Extensions;

namespace UBA.Panel.Report.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext(builder.Configuration);
        builder.Services.AddMediatR();
        builder.Services.AddInfrastructureServices(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        using (var scope = app.Services.CreateScope()) 
        { 
            var services = scope.ServiceProvider;
        
            var context = services.GetRequiredService<ReportsDbContext>(); 
            context.Database.EnsureCreated();
            
            var blobContainerClient = services.GetRequiredService<BlobContainerClient>();
            blobContainerClient.CreateIfNotExists();
        }
        
        app.Run();
    }
}