# Wypożyczalnia Sprzętu Uczelnianego

Projekt02 z przedmiotu APBD - prosta aplikacja konsolowa do obsługi wypożyczalni sprzętu na uczelni. Jej głównym celem jest pokazanie w praktyce dobrych nawyków programowania obiektowego.

## Instrukcja uruchomienia

Program można uruchomić na dwa sposoby:

* Przez terminal: Wejdź do głównego folderu projektu i wpisz komendę `dotnet run`.
* W środowisku IDE (np. JetBrains Rider lub Visual Studio): Otwórz projekt i po prostu kliknij zieloną strzałkę Run (Uruchom) na górnym pasku.

## Architektura i Decyzje Projektowe

Projekt został podzielony na wyraźne warstwy, aby oddzielić interfejs od logiki biznesowej:
1. **Domain (Model Domeny):** Czyste klasy reprezentujące byty (Sprzęt, Użytkownik, Wypożyczenie). Nie zawierają logiki operacyjnej.
2. **Services (Logika Biznesowa):** Klasy odpowiedzialne za reguły, przeliczanie kar i procesowanie wypożyczeń.
3. **Program.cs (Warstwa Prezentacji):** Służy wyłącznie jako punkt wejścia i wyświetlania danych w konsoli.

### 1. Kohezja i Odpowiedzialność (SRP)
W modelu domeny, obiekt `Rental` sam kontroluje swój stan. Data zwrotu (`ReturnDate`) może zostać zmieniona wyłącznie przez dedykowaną metodę `ReturnEquipment()`, a nie przez publiczny setter. Dzięki temu stan obiektu jest bezpieczny i spójny.

### 2. Coupling (Luźne powiązania) i OCP
Aby uniknąć twardych powiązań między klasą zarządzającą wypożyczeniami (`RentalService`) a konkretnymi regułami biznesowymi, wydzieliłem te reguły za pomocą interfejsów:
* `IPenaltyCalculator`
* `IUserLimitValidator`

`RentalService` przyjmuje te interfejsy w konstruktorze (Dependency Injection). Dzięki temu, jeśli uczelnia zmieni zasady naliczania kar, wystarczy dopisać nową klasę implementującą `IPenaltyCalculator`, bez konieczności ingerowania w kod samego serwisu wypożyczeń.

### 3. Dziedziczenie w Modelu Domeny
Dziedziczenie zostało użyte tylko tam, gdzie wynika to bezpośrednio z domeny.
* Klasa abstrakcyjna `Equipment` przechowuje wspólne cechy (Id, Name, Status), natomiast konkretne klasy (`Laptop`, `Camera`, `Projector`) dodają swoje specyficzne właściwości.
* Podobnie klasa abstrakcyjna `User` stanowi bazę dla `Student` i `Employee`.