using System;
using System.Collections.Generic;
using Moq;
using VestingShares.Models;
using VestingShares.Utilities;
using Xunit;

namespace VestingShares.Tests
{
    public class VestingShares_Tests
    {
        [Fact]
        public void GetVestedSharesByDate_Should_Return_Correct_Results()
        {            
            var utility = new Mock<ICsvReaderUtility>();
            utility.Setup(u => u.ReadEventsIntoList(It.IsAny<string>())).Returns(
                new List<VestEvent>(){new VestEvent(){AwardId =  "ISO-001", EmployeeName =  "Alice Smith", EmployeeId = "E001", Quantity = 1000}}
            );
            var vestedSharesApplication = new VestingShares.Application.VestedSharesApplication(utility.Object);
            var result = vestedSharesApplication.GetVestedSharesByDate(DateTime.Parse("2020-04-01"), "test-files/example.csv"); 
            Assert.Equal(result[0].AwardId, "ISO-001");
            Assert.Equal(result[0].EmployeeId,"E001");
            Assert.Equal(result[0].EmployeeName,"Alice Smith");
            Assert.Equal(result[0].Quantity,"1000"); 
        }

        [Fact]
        public void GetVestedSharesByDate_Should_Return_Correct_Quantity()
        {
            var utility = new Mock<ICsvReaderUtility>();
            utility.Setup(u => u.ReadEventsIntoList(It.IsAny<string>())).Returns(
                new List<VestEvent>(){new VestEvent(){AwardId =  "ISO-001", EmployeeName =  "Alice Smith", EmployeeId = "E001", Quantity = 1000}
                                    , new VestEvent(){ AwardId =  "ISO-001", EmployeeName =  "Alice Smith", EmployeeId = "E001", Quantity = -700 }}
            );
            var vestedSharesApplication = new VestingShares.Application.VestedSharesApplication(utility.Object);
            var result = vestedSharesApplication.GetVestedSharesByDate(DateTime.Parse("2021-04-01"), "test-files/example2.csv"); 
            Assert.Equal(result[0].AwardId,"ISO-001");
            Assert.Equal(result[0].EmployeeId,"E001");
            Assert.Equal(result[0].EmployeeName,"Alice Smith");
            Assert.Equal("300", result[0].Quantity); 
        }

        [Fact]
        public void GetVestedSharesByDate_Should_Return_Correct_Precision()
        {
            var utility = new Mock<ICsvReaderUtility>();
            utility.Setup(u => u.ReadEventsIntoList(It.IsAny<string>())).Returns(
                new List<VestEvent>(){new VestEvent(){AwardId =  "ISO-001", EmployeeName =  "Alice Smith", EmployeeId = "E001", Quantity = decimal.Parse("299.75")}}
            );
            var vestedSharesApplication = new VestingShares.Application.VestedSharesApplication(utility.Object);
            var result = vestedSharesApplication.GetVestedSharesByDate(DateTime.Parse("2021-01-01"), "test-files/example3.csv", 1); 
            Assert.Equal(result[0].AwardId,"ISO-001");
            Assert.Equal(result[0].EmployeeId,"E001");
            Assert.Equal(result[0].EmployeeName,"Alice Smith");
            Assert.Equal(result[0].Quantity,"299.8"); 
        }
    }
}
