using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using UBA.Panel.Report.Domain.Data.ValueObjects;
using UBA.Panel.Report.Infrastructure.ClassMaps.TypeConverters;
using DateTimeConverter = CsvHelper.TypeConversion.DateTimeConverter;

namespace UBA.Panel.Report.Infrastructure.ClassMaps;

public sealed class ReportItemClassMap : ClassMap<ReportItem>
{
    public ReportItemClassMap()
    {
        Map(r => r.RowId).Name("Id");
        Map(r => r.ParentPaper).Name("Parent paper");
        Map(r => r.Uuid).Name("Uuid");
        Map(r => r.StartIn).Name("Start in");
        Map(r => r.CompanyName).Name("Company name");
        Map(r => r.RegistrationDate).Name("Registration date").TypeConverter<DateConverter>();
        Map(r => r.Vin).Name("Vin");
        Map(r => r.IsElectric).Name("Electric").TypeConverter<BoolConverter>();
        Map(r => r.FirstName).Name("First name");
        Map(r => r.VehicleClass).Name("Vehicle class");
        Map(r => r.MappedVehicleClass).Name("Mapped vehicle class");
        Map(r => r.CreatedAt).Name("Created at").TypeConverter<DateTimeConverter>();
        Map(r => r.FrontFile).Name("Front file");
        Map(r => r.Plan).Name("Plan");
        Map(r => r.AutoExtendedContract).Name("Auto extend contract");
        Map(r => r.ThirdParty).Name("Third party");
        Map(r => r.UpdatedAt).Name("Updated at").TypeConverter<DateTimeConverter>();
        Map(r => r.Status).Name("Status").TypeConverter<StatusEnumConverter>();
        Map(r => r.Value).Name("Value");
        Map(r => r.RejectionReason).Name("Rejection reason");
        Map(r => r.EngineType).Name("Engine type");
        Map(r => r.EngineTypeCode).Name("Engine type code");
        Map(r => r.VinChecksumValid).Name("Vin checksum valid");
        Map(r => r.LicensePlateNumber).Name("License plate number");
        Map(r => r.Brand).Name("Brand");
        Map(r => r.Manufacturer).Name("Manufacturer");
        Map(r => r.TradeName).Name("Trade name");
        Map(r => r.User).Name("User");
        Map(r => r.RefferedBy).Name("Referred by");
        Map(r => r.UserName).Name("User name");
        Map(r => r.Role).Name("Role");
        Map(r => r.PayoutEnabled).Name("Payout enabled");
        Map(r => r.CommissionType).Name("Commission type");
        Map(r => r.Commission).Name("Commission");
        Map(r => r.MinPay).Name("Min pay");
        Map(r => r.AccountHolder).Name("Account holder");
        Map(r => r.Iban).Name("Iban");
    }
}

