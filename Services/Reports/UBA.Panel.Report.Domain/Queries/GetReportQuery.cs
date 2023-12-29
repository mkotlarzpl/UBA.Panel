using MediatR;
using UBA.Panel.Report.Domain.DTOs;

namespace UBA.Panel.Report.Domain.Queries;

public record GetReportQuery(string Name) : IRequest<ReportDto?>;