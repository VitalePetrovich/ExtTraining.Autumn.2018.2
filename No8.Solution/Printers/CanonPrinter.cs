using System;
using System.IO;

namespace No8.Solution
{
    internal class CanonPrinter : Printer
    {
        public CanonPrinter(string model) : base("Canon", model) { }

        public override void Print(FileStream fs)
        {
            if (fs == null)
                throw new ArgumentNullException(nameof(fs));
            
            for (int i = 0; i < fs.Length; i++)
            {
                // simulate printing
                Console.WriteLine(fs.ReadByte());
            }
        }
    }
}