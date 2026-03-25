using System;
using System.Collections.Generic;
using System.Linq;
using EquipmentRental.Domain;

namespace EquipmentRental.Services;

public class RentalService
{
    // lista wszystkich wypożyczeń w systemie
    private readonly List<Rental> _rentals = new();
    
    private readonly IUserLimitValidator _limitValidator;
    private readonly IPenaltyCalculator _penaltyCalculator;

    public RentalService(IUserLimitValidator limitValidator, IPenaltyCalculator penaltyCalculator)
    {
        _limitValidator = limitValidator ?? throw new ArgumentNullException(nameof(limitValidator));
        _penaltyCalculator = penaltyCalculator ?? throw new ArgumentNullException(nameof(penaltyCalculator));
    }

    public IReadOnlyList<Rental> GetAllRentals() => _rentals.AsReadOnly();

    public Rental RentEquipment(User user, Equipment equipment, int days)
    {
        if (equipment.Status != EquipmentStatus.Available)
        {
            throw new InvalidOperationException($"Sprzęt '{equipment.Name}' nie jest obecnie dostępny do wypożyczenia.");
        }

        int activeRentalsCount = _rentals.Count(r => r.User.Id == user.Id && !r.ReturnDate.HasValue);

        if (!_limitValidator.CanUserRentMore(user, activeRentalsCount))
        {
            throw new InvalidOperationException($"Użytkownik {user.FirstName} {user.LastName} przekroczył swój limit wypożyczeń.");
        }

        var rental = new Rental(user, equipment, days);
        equipment.ChangeStatus(EquipmentStatus.Rented);
        
        _rentals.Add(rental);
        return rental;
    }

    public decimal ReturnEquipment(Rental rental)
    {
        rental.ReturnEquipment();
        rental.Equipment.ChangeStatus(EquipmentStatus.Available);
        return _penaltyCalculator.CalculatePenalty(rental);
    }
}