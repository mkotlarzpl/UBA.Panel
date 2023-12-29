using UBA.Panel.Report.Domain.Data.ValueObjects;
using UBA.Panel.Report.Domain.Interfaces;

namespace UBA.Panel.Report.Domain.Data.AggregateRoots;

public class Report : IAggregateRoot
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public List<ReportItem> ReportItems { get; set; } = new List<ReportItem>();
    
    public DateTime CreatedAt { get; set; }

    public Report(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddReportItem(ReportItem reportItem, IVinChecksumCalculator vinChecksumCalculator)
    {
        reportItem.ReportId = Id;
        reportItem.CreatedAt = DateTime.UtcNow;
        reportItem.VinCheckDigit = vinChecksumCalculator.Calculate(reportItem.Vin);
        ReportItems.Add(reportItem);
    }
}