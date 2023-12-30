using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace UBA.Panel.Report.Infrastructure.ClassMaps.TypeConverters;

internal sealed class DateTimeConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrEmpty(text))
        {
            return DateTime.MinValue.ToUniversalTime();
        }

        return DateTime.Parse(text, CultureInfo.InvariantCulture).ToUniversalTime();
    }
}