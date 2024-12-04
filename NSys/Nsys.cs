using System;
using System.Text;

public class NSysException : Exception
{
    public NSysException(string message) : base(message) { }
}
public class NSystem
{
    private const string Stellenwerte = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public ulong Dezimalwert { get; private set; }
    public int Zahlensystem { get; private set; }

    #region Konstruktoren
    // Standardkonstruktor: Dezimalwert 0, Basis 10
    public NSystem()
    {
        Dezimalwert = 0;
        Zahlensystem = 10;
    }

    // Gegebenem Dezimalwert und Basis
    public NSystem(ulong decimalValue, int baseSystem)
    {
        ValidateBase(baseSystem); //Checkt, ob die Basis im gültigen Bereich liegt
        Dezimalwert = decimalValue;
        Zahlensystem = baseSystem;
    }
    #endregion Konstruktoren

    // Erzeugt Objekt aus einer Zahl im Zielsystem (als String!!!)
    public NSystem(string valueInBase, int baseSystem)
    {
        ValidateBase(baseSystem);
        Dezimalwert = ConvertFromBase(valueInBase, baseSystem);
        Zahlensystem = baseSystem;
    }

    #region Umwandlungen
    // Wandelt eine Dezimalzahl in ein beliebiges Stellenwertsystem um und gibt Ergebnis als String zurück
    private static string ConvertToBase(ulong number, int baseSystem)
    {
        if (number == 0) return "0";

        var result = new StringBuilder();
        while (number > 0)
        {
            int temp = (int)(number % (ulong)baseSystem);
            result.Insert(0, Stellenwerte[temp]);
            number /= (ulong)baseSystem;
        }

        return result.ToString();
    }

    // Wandelt eine Zahlenwertsystem-Zahl (ICh hoffe du weißt, was ich meine :D) in eine Dezimalzahl um und gibt Ergebnis als Integer (Long) zurück
    private static ulong ConvertFromBase(string numberInBase, int baseSystem)
    {
        ulong result = 0;
        foreach (char c in numberInBase.ToUpper())
        {
            int value = Stellenwerte.IndexOf(c);
            if (value == -1 || value >= baseSystem)
                throw new ArgumentException($"Ungültiges Zeichen '{c}' für das Stellenwertsystem mit Basis {baseSystem}.");
            result = result * (ulong)baseSystem + (ulong)value;
        }

        return result;
    }
    #endregion Umwandlungen

    #region Operatorüberladungen
    // Sinnvolle Operatorüberladungen:
    public static NSystem operator +(NSystem a, NSystem b)
    {
        return new NSystem(a.Dezimalwert + b.Dezimalwert, a.Zahlensystem);
    }

    public static NSystem operator -(NSystem a, NSystem b)
    {
        if (a.Dezimalwert < b.Dezimalwert)
            throw new NSysException("Ergebnis kann nicht negativ sein.");
        return new NSystem(a.Dezimalwert - b.Dezimalwert, a.Zahlensystem);
    }

    public static NSystem operator *(NSystem a, NSystem b)
    {
        return new NSystem(a.Dezimalwert * b.Dezimalwert, a.Zahlensystem);
    }

    public static NSystem operator /(NSystem a, NSystem b)
    {
        if (b.Dezimalwert == 0)
            throw new DivideByZeroException("Division durch Null ist nicht erlaubt.");
        return new NSystem(a.Dezimalwert / b.Dezimalwert, a.Zahlensystem);
    }

    public static NSystem operator ++(NSystem a)
    {
        return new NSystem(a.Dezimalwert + 1, a.Zahlensystem);
    }

    public static NSystem operator --(NSystem a)
    {
        if (a.Dezimalwert == 0)
            throw new NSysException("Wert kann nicht unter 0 sinken.");
        return new NSystem(a.Dezimalwert - 1, a.Zahlensystem);
    }
    // << und >> sind in diesem Fall nicht sinnvoll, da sie Bits verschieben und damit die Struktur unserer Darstellung des Zahlenwertsystems zerstören
    #endregion Operatorüberladungen

    // Gibt Objekt als String aus
    public override string ToString()
    {
        return ConvertToBase(Dezimalwert, Zahlensystem);
    }

    #region Exceptions
    // Wirft eine Exception, wenn die Basis nicht im gültigen Bereich liegt
    private static void ValidateBase(int baseSystem)
    {
        if (baseSystem < 2 || baseSystem > 36)
            throw new ArgumentOutOfRangeException(nameof(baseSystem), "Basis muss zwischen 2 und 36 liegen.");
    }
    #endregion Exceptions
}

public class NSys16 : NSystem
{
    public NSys16(string hexValue) : base(hexValue, 16) { }

    public NSys16(ulong decimalValue) : base(decimalValue, 16) { }
}