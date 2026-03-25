using System;
using EquipmentRental.Domain;

namespace EquipmentRental.Services;

public class StandardPenaltyCalculator : IPenaltyCalculator
{
    private const decimal DailyPenaltyRate = 10.0m; 

    public decimal CalculatePenalty(Rental rental)
    {
        if (!rental.IsOverdue)
        {
            return 0m;
        }
        
        DateTime endDate = rental.ReturnDate ?? DateTime.Now;
        int overdueDays = (endDate - rental.DueDate).Days;

        return overdueDays > 0 ? overdueDays * DailyPenaltyRate : 0m;
    }
}