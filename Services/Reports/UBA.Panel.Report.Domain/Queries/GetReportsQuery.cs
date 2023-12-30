using UBA.Panel.Report.Common.DTOs;

namespace UBA.Panel.Report.Domain.Queries;

public record GetReportsQuery(int? Page) 
    : BasePaginatedQuery<IEnumerable<ReportDto>>(Page);