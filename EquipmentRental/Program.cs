using System;
using System.Linq;
using EquipmentRental.Domain;
using EquipmentRental.Services;

Console.WriteLine("=== SYSTEM WYPOŻYCZALNI SPRZĘTU ===\n");

var limitValidator = new UserLimitValidator();
var penaltyCalculator = new StandardPenaltyCalculator();
var rentalService = new RentalService(limitValidator, penaltyCalculator);

var laptop = new Laptop("Dell XPS 15", "Intel Core i7", 16);
var projector = new Projector("Epson 4K", "3840x2160", 3000);
var camera = new Camera("Sony A7 III", true, "E-Mount");

var student = new Student("Jan", "Kowalski", "s12345");
var employee = new Employee("Anna", "Nowak", "IT Department");

Console.WriteLine("--- POPRAWNE WYPOŻYCZENIE ---");
var rental1 = rentalService.RentEquipment(student, laptop, 7);
Console.WriteLine($"{student.FirstName} wypożyczył: {laptop.Name}. Termin zwrotu: {rental1.DueDate.ToShortDateString()}");

Console.WriteLine("\n--- PRÓBA WYPOŻYCZENIA NIEDOSTĘPNEGO SPRZĘTU ---");
try
{
    rentalService.RentEquipment(employee, laptop, 3); 
}
catch (Exception ex)
{
    Console.WriteLine($"BŁĄD (Złapany wyjątek): {ex.Message}");
}

Console.WriteLine("\n--- PRÓBA PRZEKROCZENIA LIMITU ---");
try
{
    var rental2 = rentalService.RentEquipment(student, camera, 2);
    Console.WriteLine($"{student.FirstName} wypożyczył aparat. Ma teraz 2 aktywne wypożyczenia.");
    rentalService.RentEquipment(student, projector, 2); 
}
catch (Exception ex)
{
    Console.WriteLine($"BŁĄD (Złapany wyjątek): {ex.Message}");
}

Console.WriteLine("\n--- ZWROT W TERMINIE ---");
var activeCameraRental = rentalService.GetAllRentals().First(r => r.Equipment.Id == camera.Id && !r.ReturnDate.HasValue);
decimal penalty1 = rentalService.ReturnEquipment(activeCameraRental);
Console.WriteLine($"Zwrócono: {camera.Name}. Naliczona kara: {penalty1} zł");

Console.WriteLine("\n--- ZWROT OPÓŹNIONY (KARA) ---");
var delayedRental = rentalService.RentEquipment(employee, projector, -5);
decimal penalty2 = rentalService.ReturnEquipment(delayedRental);
Console.WriteLine($"Zwrócono opóźniony sprzęt: {projector.Name}. Naliczona kara: {penalty2} zł");

Console.WriteLine("\n--- RAPORT KOŃCOWY ---");
Console.WriteLine($"Wszystkie operacje zarejestrowane w systemie: {rentalService.GetAllRentals().Count}");
foreach (var r in rentalService.GetAllRentals())
{
    string status = r.ReturnDate.HasValue ? "Zwrócony" : "Wypożyczony";
    Console.WriteLine($"- Użytkownik: {r.User.FirstName} | Sprzęt: {r.Equipment.Name} | Status: {status}");
}