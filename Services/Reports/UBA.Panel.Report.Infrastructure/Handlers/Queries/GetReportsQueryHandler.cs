using MediatR;
using Microsoft.Extensions.Logging;
using UBA.Panel.Report.Common.DTOs;
using UBA.Panel.Report.Domain.Interfaces;
using UBA.Panel.Report.Domain.Queries;

namespace UBA.Panel.Report.Infrastructure.Handlers.Queries;

public class GetReportsQueryHandler : IRequestHandler<GetReportsQuery, IEnumerable<ReportDto>>
{
    private readonly IReportsRepository _repository;
    private readonly ILogger<GetReportsQueryHandler> _logger;

    public GetReportsQueryHandler(IReportsRepository repository, ILogger<GetReportsQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public Task<IEnumerable<ReportDto>> Handle(GetReportsQuery request, CancellationToken cancellationToken)
    {
        var entities = _repository.GetReports(request.Page ?? 1);
        return Task.FromResult(entities.Select(r => new ReportDto(r.Id, r.Name)));
    }
}