namespace UBA.Panel.Report.Domain.DTOs;

public record ReportDetailsDto(Guid Id, String Name, IEnumerable<ReportItemDto> Items);