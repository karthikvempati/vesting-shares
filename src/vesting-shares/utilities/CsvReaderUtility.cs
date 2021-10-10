using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System;
using VestingShares.Models;
using CsvHelper;
using System.Linq;
using CsvHelper.Configuration;

namespace VestingShares.Utilities
{
    public interface ICsvReaderUtility
    {
        IEnumerable<VestEvent> ReadEventsIntoList(string filePath);
    }

    public class CsvReaderUtility : ICsvReaderUtility
    {
        public IEnumerable<VestEvent> ReadEventsIntoList(string filePath)
        {
            IEnumerable<VestEvent> events;

            using (var reader = new StreamReader(filePath))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                };
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Context.RegisterClassMap<EventMap>();
                    var record = new VestEvent();
                    var records = csv.EnumerateRecords(record);
                    events = records
                            .Select(r => new VestEvent()
                            {
                                AwardId = r.AwardId,
                                EmployeeId = r.EmployeeId,
                                EmployeeName = r.EmployeeName,
                                EventType = r.EventType,
                                Quantity = r.EventType == "VEST" ? r.Quantity : r.Quantity * -1,
                                VestDate = r.VestDate
                            })
                            .ToList();
                }
            }
            return events;
        }
    }
}