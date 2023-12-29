using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using UBA.Panel.Report.FileProcessor.Functions;
using UBA.Panel.Report.Infrastructure.Extensions;

[assembly: FunctionsStartup(typeof(Startup))]

namespace UBA.Panel.Report.FileProcessor.Functions;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var context = builder.GetContext();
        builder.Services.AddDbContext(context.Configuration);
        builder.Services.AddFileProcessorServices(context.Configuration);
    }
}