using MediatR;
using Microsoft.Extensions.Logging;
using UBA.Panel.Report.Common.DTOs;
using UBA.Panel.Report.Domain.Interfaces;
using UBA.Panel.Report.Domain.Queries;

namespace UBA.Panel.Report.Infrastructure.Handlers.Queries;

public class GetReportDetailsQueryHandler : IRequestHandler<GetReportDetailsQuery, ReportDetailsDto>
{
    private readonly IReportsRepository _repository;
    private readonly ILogger<GetReportDetailsQueryHandler> _logger;

    public GetReportDetailsQueryHandler(IReportsRepository repository, ILogger<GetReportDetailsQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task<ReportDetailsDto> Handle(GetReportDetailsQuery request, CancellationToken cancellationToken)
    {
        var report = await _repository.GetReportWithDetails(request.ReportId);
        var itemsCount = _repository.GetRecordItemsTotalForReport(request.ReportId);

        return new ReportDetailsDto(report.Id, report.Name,
            new PagedResultDto<ReportItemDto>(1, itemsCount, IReportsRepository.DefaultPageSize,
                report.ReportItems.Select(ri => new ReportItemDto
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