using System;

namespace VestingShares.Models
{
    public class VestEvent
    {
        public string EventType { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string AwardId { get; set; }
        public DateTime VestDate { get; set; }
        public decimal Quantity { get; set; }        
    }
}
