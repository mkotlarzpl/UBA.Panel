using MediatR;
using Microsoft.Extensions.Logging;
using UBA.Panel.Report.Common.DTOs;
using UBA.Panel.Report.Domain.Data.ValueObjects;
using UBA.Panel.Report.Domain.Interfaces;
using UBA.Panel.Report.Domain.Queries;

namespace UBA.Panel.Report.Infrastructure.Handlers.Queries;

public class GetReportItemsElectricsForReportQueryHandler : 
    IRequestHandler<GetReportItemsElectricsForReportQuery, PagedResultDto<ReportItemDto>>
{
    private readonly IReportsRepository _repository;
    private readonly ILogger<GetReportItemsWithDuplicatedVinsForReportQuery> _logger;

    public GetReportItemsElectricsForReportQueryHandler(IReportsRepository repository,
        ILogger<GetReportItemsWithDuplicatedVinsForReportQuery> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public Task<PagedResultDto<ReportItemDto>> Handle(GetReportItemsElectricsForReportQuery request, CancellationToken cancellationToken)
    {
        var reportItems = _repository.GetReportItemsElectricsForReport(request.ReportId, request.Page);
        var totalItems = _repository.GetRecordItemsTotalForReport(request.ReportId);

        return Task.FromResult(new PagedResultDto<ReportItemDto>(request.Page, totalItems, IReportsRepository.DefaultPageSize,
            reportItems.Select(CreateReportItemDto)));
    }

    private ReportItemDto CreateReportItemDto(ReportItem ri) =>
        new ReportItemDto
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
        };
}