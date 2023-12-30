namespace UBA.Panel.Report.Common.DTOs;

public record ReportDetailsDto(Guid Id, String Name, PagedResultDto<ReportItemDto> ReportItems);