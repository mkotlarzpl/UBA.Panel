using MediatR;
using Microsoft.Extensions.Logging;
using UBA.Panel.Report.Common.DTOs;
using UBA.Panel.Report.Domain.Interfaces;
using UBA.Panel.Report.Domain.Queries;
using UBA.Panel.Report.Infrastructure.Interfaces;

namespace UBA.Panel.Report.Infrastructure.Handlers.Queries;

public class GetReportDetailsQueryHandler : IRequestHandler<GetReportDetailsQuery, ReportDetailsDto>
{
    private readonly IReportsRepository _repository;
    private readonly IReportItemDtoFactory _reportItemDtoFactory;

    public GetReportDetailsQueryHandler(IReportsRepository repository, IReportItemDtoFactory reportItemDtoFactory)
    {
        _repository = repository;
        _reportItemDtoFactory = reportItemDtoFactory;
    }
    
    public async Task<ReportDetailsDto> Handle(GetReportDetailsQuery request, CancellationToken cancellationToken)
    {
        var report = await _repository.GetReportWithDetails(r => r.Id == request.ReportId);
        var itemsCount = _repository.GetRecordItemsTotalForReport(request.ReportId);

        return new ReportDetailsDto(report.Id, report.Name,
            new PagedResultDto<ReportItemDto>(1, itemsCount, IReportsRepository.DefaultPageSize,
                report.ReportItems.Select(_reportItemDtoFactory.Create)));
    }
}