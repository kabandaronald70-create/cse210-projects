using Fractions;

Fraction f1 = new Fraction();
Fraction f2 = new Fraction(6);
Fraction f3 = new Fraction(6, 7);


Console.WriteLine("=== Testing getters/setters ===");
Fraction testFraction = new Fraction(2, 3);
Console.WriteLine($"Before setter: {testFraction.GetFractionString()}");
testFraction.SetTop(5);
testFraction.SetBottom(8);
Console.WriteLine($"After setter:  {testFraction.GetFractionString()}");
Console.WriteLine($"Retrieved Top: {testFraction.GetTop()}");
Console.WriteLine($"Retrieved Bottom: {testFraction.GetBottom()}");
Console.WriteLine();

Console.WriteLine("=== Fraction representations ===");

Fraction[] samples = {
    new Fraction(),
    new Fraction(5),
    new Fraction(3, 4),
    new Fraction(1, 3)
};

foreach (var frac in samples)
{
    Console.WriteLine($"{frac.GetFractionString()} → {frac.GetDecimalValue()}");
}