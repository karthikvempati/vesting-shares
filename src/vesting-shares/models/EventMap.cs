using System;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace VestingShares.Models
{
    public class StringToDateConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return DateTime.ParseExact(text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return value.ToString();
        }
    }
    public class EventMap : ClassMap<VestEvent>
    {
        public EventMap()
        {
            Map(m => m.EventType).Index(0);
            Map(m => m.EmployeeId).Index(1);
            Map(m => m.EmployeeName).Index(2);
            Map(m => m.AwardId).Index(3);
            Map(m => m.VestDate).Index(4).TypeConverter<StringToDateConverter<string>>().Optional();
            Map(m => m.Quantity).Index(5);
        }
    }
}