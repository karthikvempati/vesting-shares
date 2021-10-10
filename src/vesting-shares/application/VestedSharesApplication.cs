using System;
using System.Collections.Generic;
using VestingShares.Models;
using VestingShares.Utilities;
using System.Linq;
using System.Globalization;

namespace VestingShares.Application
{
    public interface IVestedSharesApplication
    {
        List<VestedShare> GetVestedSharesByDate(DateTime inputDate, string inputFilePath);

        List<VestedShare> GetVestedSharesByDate(DateTime inputDate, string inputFilePath, int precision = 2);
    }

    public class VestedSharesApplication : IVestedSharesApplication
    {

        private ICsvReaderUtility csvReaderUtility;

        public VestedSharesApplication(ICsvReaderUtility csvReaderUtility)
        {
            this.csvReaderUtility = csvReaderUtility;
        }

        public List<VestedShare> GetVestedSharesByDate(DateTime inputDate, string inputFilePath)
        {
            var events = csvReaderUtility.ReadEventsIntoList(inputFilePath);

            var vestedShares = events
                                    .GroupBy(e => e.AwardId)
                                    .Select(ev => new VestedShare
                                    {
                                        AwardId = ev.First().AwardId,
                                        EmployeeId = ev.First().EmployeeId,
                                        EmployeeName = ev.First().EmployeeName,
                                        Quantity = (ev.Where(e => e.VestDate <= inputDate).Sum(v => v.Quantity)).ToString()
                                    });

            return vestedShares.ToList();
        }

        public List<VestedShare> GetVestedSharesByDate(DateTime inputDate, string inputFilePath, int precision = 0)
        {
            var events = csvReaderUtility.ReadEventsIntoList(inputFilePath);

            NumberFormatInfo setPrecision = new NumberFormatInfo();
            setPrecision.NumberDecimalDigits = precision;

            var vestedShares = events
                                    .GroupBy(e => e.AwardId)
                                    .Select(ev => new VestedShare
                                    {
                                        AwardId = ev.First().AwardId,
                                        EmployeeId = ev.First().EmployeeId,
                                        EmployeeName = ev.First().EmployeeName,
                                        Quantity = (ev.Where(e => e.VestDate <= inputDate).Sum(v => v.Quantity)).ToString("N", setPrecision)
                                    });

            return vestedShares.ToList();
        }
    }
}