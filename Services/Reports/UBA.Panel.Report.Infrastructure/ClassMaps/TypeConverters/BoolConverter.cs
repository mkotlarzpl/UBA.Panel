using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace UBA.Panel.Report.Infrastructure.ClassMaps.TypeConverters;

public class BoolConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrEmpty(text))
        {
            return false;
        }

        return Convert.ToBoolean(text);
    }
}