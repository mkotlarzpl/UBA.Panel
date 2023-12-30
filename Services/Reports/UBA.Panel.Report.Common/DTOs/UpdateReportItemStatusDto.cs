using UBA.Panel.Report.Common.Enums;

namespace UBA.Panel.Report.Common.DTOs;

public record UpdateReportItemStatusDto(Guid ReportItemId, StatusEnum Status);