using MediatR;

namespace UBA.Panel.Report.Domain.Queries;

public record DownloadReportQuery(Guid ReportId, string Format) : IRequest<MemoryStream>;