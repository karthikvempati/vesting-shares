using System;
using System.Collections.Generic;
using VestingShares.Models;

public static class PrintUtility
{
    public static void PrintVestedShares(IList<VestedShare> vestedShares)
    {
        foreach (var vestedShare in vestedShares)
        {
            Console.WriteLine($"{vestedShare.EmployeeId},{vestedShare.EmployeeName},{vestedShare.AwardId}, {vestedShare.Quantity}");
        }
    }
}