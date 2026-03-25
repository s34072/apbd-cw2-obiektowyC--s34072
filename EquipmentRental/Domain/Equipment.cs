namespace EquipmentRental.Domain;

public abstract class Equipment
{
    public Guid Id { get; } 
    public string Name { get; } 
    public EquipmentStatus Status { get; private set; } 

    protected Equipment(string name)
    {
        Id = Guid.NewGuid(); // System sam generuje ID 
        Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Name cannot be empty") : name;
        Status = EquipmentStatus.Available; // Domyślnie nowy sprzęt jest dostępny
    }

    public void ChangeStatus(EquipmentStatus newStatus)
    {
        Status = newStatus;
    }
}