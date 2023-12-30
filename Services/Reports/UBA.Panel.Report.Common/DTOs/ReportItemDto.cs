using UBA.Panel.Report.Common.Enums;

namespace UBA.Panel.Report.Common.DTOs;

public class ReportItemDto
{
    public Guid Id { get; set; }
    public int RowId { get; set; }
    public string FirstName { get; set; }
    public string CompanyName { get; set; }
    public string Vin { get; set; }
    public char VinCheckDigit { get; set; }
    public string CompleteVin { get; set; }
    public bool IsElectric { get; set; }
    public string MappedVehicleClass { get; set; }
    public string FrontFile { get; set; }
    public StatusEnum Status { get; set; }
    public string Source { get; set; }
}