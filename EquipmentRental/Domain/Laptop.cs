namespace EquipmentRental.Domain;

public class Laptop : Equipment
{
    public string Processor { get; }
    public int RamGb { get; }

    public Laptop(string name, string processor, int ramGb) : base(name)
    {
        Processor = processor;
        RamGb = ramGb;
    }
}