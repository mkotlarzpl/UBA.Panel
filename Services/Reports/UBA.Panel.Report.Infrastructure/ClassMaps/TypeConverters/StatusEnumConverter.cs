using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using UBA.Panel.Report.Common.Enums;

namespace UBA.Panel.Report.Infrastructure.ClassMaps.TypeConverters;

internal class StatusEnumConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        return Enum.Parse<StatusEnum>(text);
    }
}