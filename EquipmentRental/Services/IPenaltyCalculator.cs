using EquipmentRental.Domain;

namespace EquipmentRental.Services;

public interface IPenaltyCalculator
{
    decimal CalculatePenalty(Rental rental);
}