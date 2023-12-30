namespace UBA.Panel.Report.Common.DTOs;

public record PagedResultDto<T>(int CurrentPage, int Total, int PageSize, IEnumerable<T> Items);