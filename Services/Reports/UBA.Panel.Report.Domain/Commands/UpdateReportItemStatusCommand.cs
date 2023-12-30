using MediatR;
using UBA.Panel.Report.Common.Enums;

namespace UBA.Panel.Report.Domain.Commands;

public record UpdateReportItemStatusCommand(Guid ReportItemId, StatusEnum Status) : IRequest;