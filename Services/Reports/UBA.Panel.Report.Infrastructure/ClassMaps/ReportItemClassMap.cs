using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using UBA.Panel.Report.Domain.Data.ValueObjects;
using UBA.Panel.Report.Domain.Interfaces;

namespace UBA.Panel.Report.Infrastructure.ClassMaps;

public sealed class ReportItemClassMap : ClassMap<ReportItem>
{
    public ReportItemClassMap()
    {
        Map(r => r.RowId).Name("Id");
        Map(r => r.FirstName).Name("First name");
        Map(r => r.CompanyName).Name("Company name");
        Map(r => r.RegistrationDate).Name("Registration date").TypeConverter<DateConverter>();
        Map(r => r.Vin).Name("Vin");
        Map(r => r.IsElectric).Name("Electric");
        Map(r => r.VehicleClass).Name("Mapped vehicle class");
    }
}

internal sealed class DateConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrEmpty(text))
        {
            return DateTime.MinValue.ToUniversalTime();
        }
        
        return DateTime.ParseExact(text, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToUniversalTime();
    }
}