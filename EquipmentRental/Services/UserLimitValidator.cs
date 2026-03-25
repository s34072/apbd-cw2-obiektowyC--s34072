using EquipmentRental.Domain;

namespace EquipmentRental.Services;

public class UserLimitValidator : IUserLimitValidator
{
    public bool CanUserRentMore(User user, int currentActiveRentalsCount)
    {
        int limit = user switch
        {
            Student => 2,
            Employee => 5,
            _ => 0
        };

        return currentActiveRentalsCount < limit;
    }
}