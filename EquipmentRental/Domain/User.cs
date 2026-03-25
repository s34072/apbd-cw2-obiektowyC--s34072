namespace EquipmentRental.Domain;

public abstract class User
{
    public Guid Id { get; }
    public string FirstName { get; }
    public string LastName { get; } // Zwraca typ użytkownika
    public string UserType => GetType().Name;

    protected User(string firstName, string lastName)
    {
        Id = Guid.NewGuid();
        FirstName = string.IsNullOrWhiteSpace(firstName) ? throw new ArgumentException("First name cannot be empty") : firstName;
        LastName = string.IsNullOrWhiteSpace(lastName) ? throw new ArgumentException("Last name cannot be empty") : lastName;
    }
}