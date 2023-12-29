using MediatR;

namespace UBA.Panel.Report.Domain.Commands;

public record AddFileToReportCommand(Guid ReportId, string FileName, Stream File) : IRequest;