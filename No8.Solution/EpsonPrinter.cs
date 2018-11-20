using System;
using System.IO;

namespace No8
{
    internal class EpsonPrinter
    {
        public EpsonPrinter()
        {
            Model = "231";
            Name = "Epson";
        }

        public void Print(FileStream fs)
        {
            for (int i = 0; i < fs.Length; i++)
            {
                // simulate printing
                Console.WriteLine(fs.ReadByte());
            }
        }

        public string Name { get; set; }

        public string Model { get; set; }
    }
}