namespace NSys
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testprogramm für NSystem");
                try
                {
                    // Validierungsfälle: Basisprüfung
                    Console.WriteLine("Test: Ungültige Basis");
                    try
                    {
                        Console.WriteLine("Erstellen einer Basis-37-Zahl mit dem Wert 10");
                        NSystem invalidBase = new NSystem(10, 37);
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine($"Erwartete Exception: {ex.Message}");
                    }

                    Console.WriteLine("Test: Zahl mit ungültigen Zeichen");
                    try
                    {
                        Console.WriteLine("Erstellen einer Hexadezimalzahl mit dem Wert 1GZ");
                        NSystem invalidChars = new NSystem("1GZ", 16);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Erwartete Exception: {ex.Message}");
                    }

                    // Test: Arithmetische Operationen
                    Console.WriteLine("Test: Addition von zwei Zahlen");
                    NSystem hex1 = new NSystem("A", 16);
                    NSystem bin1 = new NSystem("101", 2);
                    NSystem result = hex1 + bin1;
                    Console.WriteLine($"Hexadezimal A + binär 101 = {result} (hex)");

                    Console.WriteLine("Test: Subtraktion mit negativem Ergebnis");
                    try
                    {
                        Console.WriteLine("Binär 101 - Hexadezimal A = ");
                        NSystem resultNegative = bin1 - hex1;
                    }
                    catch (NSysException ex)
                    {
                        Console.WriteLine($"Erwartete Exception: {ex.Message}");
                    }

                    Console.WriteLine("Test: Division durch Null");
                    try
                    {
                        Console.WriteLine("Hexadezimal A / Hexadezimal 0 = ");
                        NSystem divByZero = hex1 / new NSystem(0, 16);
                    }
                    catch (DivideByZeroException ex)
                    {
                        Console.WriteLine($"Erwartete Exception: {ex.Message}");
                    }

                    // Test: Inkrement und Dekrement
                    Console.WriteLine("Test: Inkrement");
                    NSystem tetvig1 = new NSystem("I", 23);
                    Console.WriteLine($"Vorher: Tetra-Vigesimal {tetvig1}");
                    tetvig1++;
                    Console.WriteLine($"Nachher: Tetra-Vigesimal {tetvig1}");

                    Console.WriteLine("Test: Dekrement bei Dezimal 0");
                    NSystem zero = new NSystem(0, 10);
                    try
                    {
                        zero--;
                    }
                    catch (NSysException ex)
                    {
                        Console.WriteLine($"Erwartete Exception: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unerwarteter Fehler: {ex.Message}");
                }

                Console.WriteLine("Testprogramm beendet.");
        }
    }
}
