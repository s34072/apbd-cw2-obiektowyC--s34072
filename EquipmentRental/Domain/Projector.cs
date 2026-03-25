namespace EquipmentRental.Domain;

public class Projector : Equipment
{
    public string Resolution { get; }
    public int Lumens { get; }

    public Projector(string name, string resolution, int lumens) : base(name)
    {
        Resolution = resolution;
        Lumens = lumens;
    }
}