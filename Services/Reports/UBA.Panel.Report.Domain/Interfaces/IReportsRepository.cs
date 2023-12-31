using System.Collections;
using System.Linq.Expressions;
using UBA.Panel.Report.Common.DTOs;
using UBA.Panel.Report.Common.Enums;
using UBA.Panel.Report.Domain.Data.ValueObjects;
using UBAReport = UBA.Panel.Report.Domain.Data.AggregateRoots.Report;

namespace UBA.Panel.Report.Domain.Interfaces;

public interface IReportsRepository
{
    public static int DefaultPageSize => 20;
    public Task<UBAReport> CreateReportAsync(UBAReport report);
    public Task<UBAReport?> GetReportAsync(Expression<Func<UBAReport, bool>> whereClause);
    public Task<UBAReport> UpdateReportAsync(UBAReport report);
    public IEnumerable<UBAReport> GetReports(int page, Expression<Func<UBAReport, bool>>? whereClause = null);
    public Task<UBAReport> GetReportWithDetails(
        Expression<Func<UBAReport, bool>>? whereClause = null,
        int? reportItemsPageSize = 20);

    public Task<UBAReport> GetReportWithDetailsForNotCertified(
        Expression<Func<UBAReport, bool>>? whereClause = null,
        int? reportItemsPageSize = 20);
    public IEnumerable<ReportItem> GetReportItemsForReport(Expression<Func<ReportItem, bool>> whereClause, int page);
    public IEnumerable<ReportItem> GetReportItemsWithDuplicatedVinsForReport(Guid reportId, int page);
    public Task<ReportItem> UpdateReportItemStatus(Guid reportItemId, StatusEnum status);
    public int GetRecordItemsTotalForReport(Guid reportId);
}