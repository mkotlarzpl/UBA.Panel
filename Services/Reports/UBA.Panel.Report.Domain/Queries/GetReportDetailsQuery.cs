using MediatR;
using UBA.Panel.Report.Common.DTOs;

namespace UBA.Panel.Report.Domain.Queries;

public record GetReportDetailsQuery(Guid ReportId) : IRequest<ReportDetailsDto>;