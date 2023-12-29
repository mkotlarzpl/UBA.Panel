using Microsoft.EntityFrameworkCore;
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
}