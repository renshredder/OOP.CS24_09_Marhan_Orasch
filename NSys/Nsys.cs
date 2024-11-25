using System;
using System.Text;

public class Nsys
{
    private readonly string digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private readonly int baseValue; // Basis des Zahlensystems
    private ulong value; // Intern gespeicherter Wert

    public Nsys(ulong value, int baseValue)
    {
        if (baseValue < 2 || baseValue > 36)
            throw new ArgumentException("Basis muss zwischen 2 und 36 liegen.");
        this.baseValue = baseValue;
        this.value = value;
    }

    public Nsys(string representation, int baseValue)
    {
        if (baseValue < 2 || baseValue > 36)
            throw new ArgumentException("Basis muss zwischen 2 und 36 liegen.");
        this.baseValue = baseValue;
        this.value = Parse(representation, baseValue);
    }

    private ulong Parse(string representation, int baseValue)
    {
        ulong result = 0;
        foreach (char c in representation)
        {
            int digitValue = digits.IndexOf(char.ToUpper(c));
            if (digitValue == -1 || digitValue >= baseValue)
                throw new FormatException("Ungültige Ziffer für die Basis.");
            result = result * (ulong)baseValue + (ulong)digitValue;
        }
        return result;
    }

    public override string ToString()
    {
        if (value == 0) return "0";

        StringBuilder sb = new StringBuilder();
        ulong current = value;
        while (current > 0)
        {
            int remainder = (int)(current % (ulong)baseValue);
            sb.Insert(0, digits[remainder]);
            current /= (ulong)baseValue;
        }

        return sb.ToString();
    }

}
