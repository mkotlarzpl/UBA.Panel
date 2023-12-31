using MediatR;
using Microsoft.Extensions.Logging;
using UBA.Panel.Report.Common.DTOs;
using UBA.Panel.Report.Domain.Data.ValueObjects;
using UBA.Panel.Report.Domain.Interfaces;
using UBA.Panel.Report.Domain.Queries;
using UBA.Panel.Report.Infrastructure.Interfaces;

namespace UBA.Panel.Report.Infrastructure.Handlers.Queries;

public class GetReportItemsElectricsForReportQueryHandler : 
    IRequestHandler<GetReportItemsElectricsForReportQuery, PagedResultDto<ReportItemDto>>
{
    private readonly IReportsRepository _repository;
    private readonly IReportItemDtoFactory _reportItemDtoFactory;

    public GetReportItemsElectricsForReportQueryHandler(IReportsRepository repository,
        IReportItemDtoFactory reportItemDtoFactory)
    {
        _repository = repository;
        _reportItemDtoFactory = reportItemDtoFactory;
    }
    
    public Task<PagedResultDto<ReportItemDto>> Handle(GetReportItemsElectricsForReportQuery request, CancellationToken cancellationToken)
    {
        var reportItems = _repository.GetReportItemsForReport(
            (ri) => ri.ReportId == request.ReportId && ri.IsElectric == true, 
            request.Page);
        var totalItems = _repository.GetRecordItemsTotalForReport(request.ReportId);

        return Task.FromResult(new PagedResultDto<ReportItemDto>(request.Page, totalItems, IReportsRepository.DefaultPageSize,
            reportItems.Select(_reportItemDtoFactory.Create)));
    }
}