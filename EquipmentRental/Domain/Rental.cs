using System;

namespace EquipmentRental.Domain;

public class Rental
{
    public Guid Id { get; }
    public User User { get; }
    public Equipment Equipment { get; }
    public DateTime RentalDate { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnDate { get; private set; } 
    public bool IsOverdue => ReturnDate.HasValue ? ReturnDate.Value > DueDate : DateTime.Now > DueDate;
    public Rental(User user, Equipment equipment, int rentalDays)
    {
        Id = Guid.NewGuid();
        User = user ?? throw new ArgumentNullException(nameof(user));
        Equipment = equipment ?? throw new ArgumentNullException(nameof(equipment));
        
        RentalDate = DateTime.Now;
        DueDate = RentalDate.AddDays(rentalDays);
    }

    public void ReturnEquipment()
    {
        if (ReturnDate.HasValue)
        {
            throw new InvalidOperationException("Ten sprzęt został już zwrócony.");
        }

        ReturnDate = DateTime.Now;
    }
}