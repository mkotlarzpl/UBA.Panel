using System.Collections;
using UBA.Panel.Report.Common.DTOs;
using UBA.Panel.Report.Common.Enums;
using UBA.Panel.Report.Domain.Data.ValueObjects;
using UBAReport = UBA.Panel.Report.Domain.Data.AggregateRoots.Report;

namespace UBA.Panel.Report.Domain.Interfaces;

public interface IReportsRepository
{
    public static int DefaultPageSize => 20;
    public Task<UBAReport> CreateReportAsync(UBAReport report);
    public Task<UBAReport?> GetReportByNameAsync(string name);
    public Task<UBAReport?> GetReportByIdAsync(Guid id);
    public Task<UBAReport> UpdateReportAsync(UBAReport report);
    public IEnumerable<UBAReport> GetReports(int page, int pageSize = 20);
    public Task<UBAReport> GetReportWithDetails(Guid reportId);
    public IEnumerable<ReportItem> GetReportItemsForReport(Guid reportId, int page);
    public IEnumerable<ReportItem> GetReportItemsWithDuplicatedVinsForReport(Guid reportId, int page);
    public IEnumerable<ReportItem> GetReportItemsElectricsForReport(Guid reportId, int page);
    public Task<ReportItem> UpdateReportItemStatus(Guid reportItemId, StatusEnum status);
    public int GetRecordItemsTotalForReport(Guid reportId);
}