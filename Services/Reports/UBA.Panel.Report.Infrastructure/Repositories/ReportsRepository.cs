using Microsoft.EntityFrameworkCore;
using UBA.Panel.Report.Common.Enums;
using UBA.Panel.Report.Domain.Data.ValueObjects;
using UBA.Panel.Report.Domain.Interfaces;
using UBA.Panel.Report.Infrastructure.Context;

namespace UBA.Panel.Report.Infrastructure.Repositories;

public class ReportsRepository : IReportsRepository
{
    private readonly ReportsDbContext _context;
    
    public ReportsRepository(ReportsDbContext context)
    {
        _context = context;
    }
    
    public async Task<Domain.Data.AggregateRoots.Report> CreateReportAsync(Domain.Data.AggregateRoots.Report report)
    {
        _context.Reports.Add(report);
        await _context.SaveChangesAsync();

        return report;
    }

    public Task<Domain.Data.AggregateRoots.Report?> GetReportByNameAsync(string name)
    {
        return _context.Reports.FirstOrDefaultAsync(r => r.Name == name);
    }

    public Task<Domain.Data.AggregateRoots.Report?> GetReportByIdAsync(Guid id)
    {
        return _context.Reports.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Domain.Data.AggregateRoots.Report> UpdateReportAsync(Domain.Data.AggregateRoots.Report report)
    {
        _context.Reports.Update(report);
        await _context.SaveChangesAsync();

        return report;
    }

    public IEnumerable<Domain.Data.AggregateRoots.Report> GetReports(int page, int pageSize = 20)
    {
        return _context.Reports.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }

    public async Task<Domain.Data.AggregateRoots.Report> GetReportWithDetails(Guid reportId)
    {
        return (await _context.Reports
            .Include(r => r.ReportItems.Take(20))
            .FirstOrDefaultAsync(r => r.Id == reportId))!;
    }

    public IEnumerable<ReportItem> GetReportItemsForReport(Guid reportId, int page)
    {
        return _context.ReportItems.Where(ri => ri.ReportId == reportId)
            .Skip((page - 1) * IReportsRepository.DefaultPageSize)
            .Take(IReportsRepository.DefaultPageSize);
    }

    public IEnumerable<ReportItem> GetReportItemsWithDuplicatedVinsForReport(Guid reportId, int page)
    {
        return _context.ReportItems
            .Where(items => items.ReportId == reportId &&
                            _context.ReportItems.Count(duplicated => items.Vin == duplicated.Vin) > 1)
            .Skip((page - 1) * IReportsRepository.DefaultPageSize)
            .Take(IReportsRepository.DefaultPageSize);
    }

    public IEnumerable<ReportItem> GetReportItemsElectricsForReport(Guid reportId, int page)
    {
        return _context.ReportItems
            .Where(items => items.ReportId == reportId && items.IsElectric == true)
            .Skip((page - 1) * IReportsRepository.DefaultPageSize)
            .Take(IReportsRepository.DefaultPageSize);
    }

    public async Task<ReportItem> UpdateReportItemStatus(Guid reportItemId, StatusEnum status)
    {
        var reportItem = _context.ReportItems.AsNoTracking().First(r => r.Id == reportItemId);
        reportItem = reportItem with
        {
            Status = status,
            UpdatedAt = DateTime.UtcNow
        };

        _context.ReportItems.Update(reportItem);
        await _context.SaveChangesAsync();

        return reportItem;
    }

    public int GetRecordItemsTotalForReport(Guid reportId)
    {
        return _context.ReportItems.Count(ri => ri.ReportId == reportId);
    }
}