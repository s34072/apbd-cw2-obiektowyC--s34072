namespace EquipmentRental.Domain;

public class Camera : Equipment
{
    public bool IsDigital { get; }
    public string LensMount { get; }

    public Camera(string name, bool isDigital, string lensMount) : base(name)
    {
        IsDigital = isDigital;
        LensMount = lensMount;
    }
}