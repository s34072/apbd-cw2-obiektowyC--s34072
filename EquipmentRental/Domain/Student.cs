namespace EquipmentRental.Domain;

public class Student : User
{
    public string StudentIdNumber { get; }

    public Student(string firstName, string lastName, string studentIdNumber) : base(firstName, lastName)
    {
        StudentIdNumber = studentIdNumber;
    }
}