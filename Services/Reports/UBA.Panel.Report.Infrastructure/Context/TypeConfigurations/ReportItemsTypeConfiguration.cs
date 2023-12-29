using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UBA.Panel.Report.Domain.Data.ValueObjects;

namespace UBA.Panel.Report.Infrastructure.Context.TypeConfigurations;

internal class ReportItemsTypeConfiguration : IEntityTypeConfiguration<ReportItem>
{
    public void Configure(EntityTypeBuilder<ReportItem> builder)
    {
        builder
            .ToTable("uba_panel_report_items")
            .HasKey("Id");

        builder
            .HasOne<Domain.Data.AggregateRoots.Report>(x => x.Report)
            .WithMany(x => x.ReportItems)
            .HasForeignKey(x => x.ReportId);

        builder
            .HasIndex(x => x.Source);
    }
}