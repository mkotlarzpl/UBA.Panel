using MediatR;
using Microsoft.Extensions.Logging;
using UBA.Panel.Report.Common.DTOs;
using UBA.Panel.Report.Domain.Interfaces;
using UBA.Panel.Report.Domain.Queries;

namespace UBA.Panel.Report.Infrastructure.Handlers.Queries;

public class GetReportItemsForReportQueryHandler : IRequestHandler<GetReportItemsForReportQuery, PagedResultDto<ReportItemDto>>
{
    private readonly IReportsRepository _repository;
    private readonly ILogger<GetReportItemsForReportQueryHandler> _logger;

    public GetReportItemsForReportQueryHandler(IReportsRepository repository,
        ILogger<GetReportItemsForReportQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public Task<PagedResultDto<ReportItemDto>> Handle(GetReportItemsForReportQuery request, CancellationToken cancellationToken)
    {
        var reportItems = _repository.GetReportItemsForReport(request.ReportId, request.Page);
        var totalItems = _repository.GetRecordItemsTotalForReport(request.ReportId);

        return Task.FromResult(new PagedResultDto<ReportItemDto>(request.Page, totalItems, IReportsRepository.DefaultPageSize,
            reportItems.Select(ri => new ReportItemDto
            {
                Id = ri.Id,
                RowId = ri.RowId,
                FirstName = ri.FirstName,
                CompanyName = ri.CompanyName,
                Vin = ri.Vin,
                VinCheckDigit = ri.VinCheckDigit,
                CompleteVin = ri.VinWithCheckDigit,
                IsElectric = ri.IsElectric,
                MappedVehicleClass = ri.MappedVehicleClass,
                FrontFile = ri.FrontFile,
                Status = ri.Status,
                Source = ri.Source   
            })));
    }
}