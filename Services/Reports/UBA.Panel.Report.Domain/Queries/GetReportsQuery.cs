using MediatR;
using UBA.Panel.Report.Domain.DTOs;

namespace UBA.Panel.Report.Domain.Queries;

public record GetReportsQuery(int? Page) 
    : BasePaginatedQuery<IEnumerable<ReportDto>>(Page);