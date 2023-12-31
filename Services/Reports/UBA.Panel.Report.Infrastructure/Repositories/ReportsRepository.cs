using System.Linq.Expressions;
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

    public Task<Domain.Data.AggregateRoots.Report?> GetReportAsync(Expression<Func<Domain.Data.AggregateRoots.Report, bool>> whereClause)
    {
        return _context.Reports.FirstOrDefaultAsync(whereClause);
    }

    public async Task<Domain.Data.AggregateRoots.Report> UpdateReportAsync(Domain.Data.AggregateRoots.Report report)
    {
        _context.Reports.Update(report);
        await _context.SaveChangesAsync();

        return report;
    }

    public IEnumerable<Domain.Data.AggregateRoots.Report> GetReports(int page, Expression<Func<Domain.Data.AggregateRoots.Report, bool>>? whereClause = null)
    {
        IQueryable<Domain.Data.AggregateRoots.Report> query = _context.Reports;
        if (whereClause != null)
        {
            query = query.Where(whereClause);
        }
        
        return query
            .Skip((page - 1) * IReportsRepository.DefaultPageSize).
            Take(IReportsRepository.DefaultPageSize)
            .ToList();
    }
    
    public async Task<Domain.Data.AggregateRoots.Report> GetReportWithDetails(
        Expression<Func<Domain.Data.AggregateRoots.Report, bool>>? whereClause = null,
        int? reportItemsPageSize = 20)
    {
        IQueryable<Domain.Data.AggregateRoots.Report> query = _context.Reports;
        query = reportItemsPageSize == null ? 
            query.Include(r => r.ReportItems.Where((ri) => ri.Status == StatusEnum.not_certified)) : 
            query.Include(r => r.ReportItems.Where((ri) => ri.Status == StatusEnum.not_certified))
                .Take(reportItemsPageSize.Value);
        
        return (await query.FirstOrDefaultAsync(whereClause ?? ((r) => true)))!;
    }

    public async Task<Domain.Data.AggregateRoots.Report> GetReportWithDetailsForNotCertified(
        Expression<Func<Domain.Data.AggregateRoots.Report, bool>>? whereClause = null,
        int? reportItemsPageSize = 20)
    {
        IQueryable<Domain.Data.AggregateRoots.Report> query = _context.Reports;
        query = reportItemsPageSize == null ? 
            query.Include(r => r.ReportItems.Where((ri) => ri.Status == StatusEnum.not_certified)) : 
            query.Include(r => r.ReportItems.Where((ri) => ri.Status == StatusEnum.not_certified))
                .Take(reportItemsPageSize.Value);
        
        return (await query.FirstOrDefaultAsync(whereClause ?? ((r) => true)))!;
    }

    public IEnumerable<ReportItem> GetReportItemsForReport(Expression<Func<ReportItem, bool>> whereClause, int page)
    {
        IQueryable<ReportItem> query = _context.ReportItems;

        if (whereClause != null)
        {
            query = query.Where(whereClause);
        }
        
        return query
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