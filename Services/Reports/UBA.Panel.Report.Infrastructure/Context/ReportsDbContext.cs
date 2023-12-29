using Microsoft.EntityFrameworkCore;
using UBA.Panel.Report.Domain.Data.ValueObjects;
using UBA.Panel.Report.Infrastructure.Context.TypeConfigurations;

namespace UBA.Panel.Report.Infrastructure.Context;

public class ReportsDbContext : DbContext
{
    public ReportsDbContext(DbContextOptions<ReportsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Domain.Data.AggregateRoots.Report> Reports { get; set; } = null!;
    public DbSet<ReportItem> ReportItems { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ReportTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ReportItemsTypeConfiguration());
    }
}