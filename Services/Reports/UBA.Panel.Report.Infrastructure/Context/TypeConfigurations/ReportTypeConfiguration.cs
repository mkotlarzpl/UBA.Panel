using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UBA.Panel.Report.Domain.Data.ValueObjects;

namespace UBA.Panel.Report.Infrastructure.Context.TypeConfigurations;

internal class ReportTypeConfiguration : IEntityTypeConfiguration<Domain.Data.AggregateRoots.Report>
{
    public void Configure(EntityTypeBuilder<Domain.Data.AggregateRoots.Report> builder)
    {
        builder
            .ToTable("uba_panel_reports")
            .HasKey(x => x.Id);

        builder
            .HasMany(x => x.ReportItems)
            .WithOne(x => x.Report)
            .HasForeignKey(x => x.ReportId);

        builder
            .HasIndex(x => x.Name)
            .IsUnique();
    }
}