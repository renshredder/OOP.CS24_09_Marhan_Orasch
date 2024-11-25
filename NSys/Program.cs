namespace NSys
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Beispiel: Basis 16
            Nsys hexNumber = new Nsys(255, 16);
            Console.WriteLine(hexNumber);

            // Beispiel: Basis 2
            Nsys binaryNumber = new Nsys("11111111", 2);
            Console.WriteLine(binaryNumber);

            // Beispiel: Basis 13
            Nsys base13Number = new Nsys(255, 13);
            Console.WriteLine(base13Number);

            // Beispiel: Basis 36
            Nsys base36Number = new Nsys(255, 36);
            Console.WriteLine(base36Number);

            System.Threading.Thread.Sleep(20000);
        }
    }
}
