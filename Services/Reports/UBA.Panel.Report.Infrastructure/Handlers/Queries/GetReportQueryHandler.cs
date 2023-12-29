using MediatR;
using Microsoft.Extensions.Logging;
using UBA.Panel.Report.Domain.DTOs;
using UBA.Panel.Report.Domain.Interfaces;
using UBA.Panel.Report.Domain.Queries;

namespace UBA.Panel.Report.Infrastructure.Handlers.Queries;

public class GetReportQueryHandler : IRequestHandler<GetReportQuery, ReportDto?>
{
    private readonly IReportsRepository _repository;
    private readonly ILogger<GetReportQueryHandler> _logger;

    public GetReportQueryHandler(IReportsRepository repository, ILogger<GetReportQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task<ReportDto?> Handle(GetReportQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetReportByNameAsync(request.Name);
        return result == null ? null : new ReportDto(Id: result.Id, Name: result.Name);
    }
}