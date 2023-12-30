using MediatR;
using UBA.Panel.Report.Common.DTOs;

namespace UBA.Panel.Report.Domain.Queries;

public record GetReportItemsElectricsForReportQuery(Guid ReportId, int Page)
    : IRequest<PagedResultDto<ReportItemDto>>;