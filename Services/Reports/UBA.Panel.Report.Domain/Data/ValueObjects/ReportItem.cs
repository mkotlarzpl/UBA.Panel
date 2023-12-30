using UBA.Panel.Report.Common.Enums;

namespace UBA.Panel.Report.Domain.Data.ValueObjects;

public record ReportItem
{
    public Guid Id { get; set; }
    public int RowId { get; set; }
    public string ParentPaper { get; set; } = string.Empty;
    public string Uuid { get; set; } = string.Empty;
    public int StartIn { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    public string Vin { get; set; } = string.Empty;
    public char VinCheckDigit { get; set; }
    public string VinWithCheckDigit => Vin.Insert(8, VinCheckDigit.ToString());
    public bool IsElectric { get; set; }
    public string VehicleClass { get; set; } = string.Empty;
    public string MappedVehicleClass { get; set; } = string.Empty;
    public string FrontFile { get; set; } = string.Empty;
    public int? Plan { get; set; }
    public bool AutoExtendedContract { get; set; }
    public bool ThirdParty { get; set; }
    public DateTime UpdatedAt { get; set; }
    public StatusEnum Status { get; set; } = StatusEnum.pending;
    public decimal? Value { get; set; }
    public string RejectionReason { get; set; } = string.Empty;
    public string EngineType { get; set; } = string.Empty;
    public int? EngineTypeCode { get; set; }
    public bool VinChecksumValid { get; set; }
    public string LicensePlateNumber { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public string TradeName { get; set; } = string.Empty;
    public int User { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool PayoutEnabled { get; set; }
    public string CommissionType { get; set; } = string.Empty;
    public decimal? Commission { get; set; }
    public decimal? MinPay { get; set; }
    public string AccountHolder { get; set; } = string.Empty;
    public string Iban { get; set; } = string.Empty;
    public string RefferedBy { get; set; } = string.Empty;
    public Guid ReportId { get; set; }
    public AggregateRoots.Report Report { get; set; } = null!;
    public string Source { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}