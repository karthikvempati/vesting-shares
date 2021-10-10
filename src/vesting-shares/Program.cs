using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using VestingShares.Application;
using VestingShares.Utilities;

namespace vesting_shares
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ICsvReaderUtility, CsvReaderUtility>()
                .AddSingleton<IVestedSharesApplication, VestedSharesApplication>()
                .BuildServiceProvider();

            var vestedSharesApplication = serviceProvider.GetService<IVestedSharesApplication>();

            var inputfilePath = args[0];
            var inputDate = DateTime.Parse(args[1]);
            var inputPrecision = args.Length > 2 ? int.Parse(args[2]) : 0;

            var sharesVested = vestedSharesApplication.GetVestedSharesByDate(inputDate, inputfilePath, inputPrecision);

            PrintUtility.PrintVestedShares(sharesVested); 
        } 
    }
}
