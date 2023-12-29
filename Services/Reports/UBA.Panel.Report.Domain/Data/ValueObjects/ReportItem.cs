using UBA.Panel.Report.Domain.Enums;

namespace UBA.Panel.Report.Domain.Data.ValueObjects;

public record ReportItem
{
    public Guid Id { get; set; }
    
    public int RowId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string CompanyName { get; set; } = string.Empty;
    
    public DateTime RegistrationDate { get; set; }

    public string Vin { get; set; } = string.Empty;
    
    public char VinCheckDigit { get; set; }

    public string VinWithCheckDigit => string.Join(Vin[..8], VinCheckDigit, Vin[7..]);

    public bool IsElectric { get; set; }

    public string VehicleClass { get; set; } = string.Empty;
    
    public Guid ReportId { get; set; }
    
    public AggregateRoots.Report Report { get; set; } = null!;

    public string Source { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; }

    public StatusEnum Status { get; set; } = StatusEnum.Unknown;
}