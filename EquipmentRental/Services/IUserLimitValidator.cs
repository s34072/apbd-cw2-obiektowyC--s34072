using EquipmentRental.Domain;

namespace EquipmentRental.Services;

public interface IUserLimitValidator
{
    bool CanUserRentMore(User user, int currentActiveRentalsCount);
}