using UBA.Panel.Report.Common.DTOs;
using UBA.Panel.Report.Domain.Data.ValueObjects;
using UBA.Panel.Report.Infrastructure.Interfaces;

namespace UBA.Panel.Report.Infrastructure.Factories;

public class ReportItemDtoFactory : IReportItemDtoFactory
{
    public ReportItemDto Create(ReportItem reportItem) =>
        new ReportItemDto
        {
            Id = reportItem.Id,
            RowId = reportItem.RowId,
            FirstName = reportItem.FirstName,
            CompanyName = reportItem.CompanyName,
            Vin = reportItem.Vin,
            VinCheckDigit = reportItem.VinCheckDigit,
            CompleteVin = reportItem.VinWithCheckDigit,
            IsElectric = reportItem.IsElectric,
            MappedVehicleClass = reportItem.MappedVehicleClass,
            FrontFile = reportItem.FrontFile,
            Status = reportItem.Status,
            Source = reportItem.Source
        };
}