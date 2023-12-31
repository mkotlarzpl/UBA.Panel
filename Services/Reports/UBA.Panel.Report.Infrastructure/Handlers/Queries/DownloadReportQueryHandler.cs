using MediatR;
using UBA.Panel.Report.Domain.Interfaces;
using UBA.Panel.Report.Domain.Queries;
using UBA.Panel.Report.Infrastructure.Interfaces;
using UBA.Panel.Report.Infrastructure.Strategies;

namespace UBA.Panel.Report.Infrastructure.Handlers.Queries;

public class DownloadReportQueryHandler : IRequestHandler<DownloadReportQuery, MemoryStream>
{
    private readonly IReportExporterFactory _reportExporterFactory;
    private readonly IReportsRepository _repository;

    public DownloadReportQueryHandler(IReportExporterFactory reportExporterFactory, IReportsRepository repository)
    {
        _reportExporterFactory = reportExporterFactory;
        _repository = repository;
    }
    
    public async Task<MemoryStream> Handle(DownloadReportQuery request, CancellationToken cancellationToken)
    {
        var exporter = _reportExporterFactory.Create(request.Format);
        var strategy = new NotCertifiedItemsExportStrategy(_repository, request.ReportId);

        var memoryStream = new MemoryStream();
        await exporter.Export(strategy, memoryStream);

        return memoryStream;
    }
}