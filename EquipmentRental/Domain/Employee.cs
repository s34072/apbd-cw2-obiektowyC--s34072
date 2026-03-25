namespace EquipmentRental.Domain;

public class Employee : User
{
    public string Department { get; }

    public Employee(string firstName, string lastName, string department) : base(firstName, lastName)
    {
        Department = department;
    }
}