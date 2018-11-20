using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using No8.Solution;

namespace No8.Solution.Console
{
    using No8.Solution.Factories;

    using Console = System.Console;

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
               ShowMenu();
        }

        static void ShowMenu()
        {
            while (true)
            {
                int i = 2;
                Console.WriteLine("1:Add new printer");
                foreach (var menuLine in PrinterManager.Printers.Select(p => $"{i++}:Print on {p.Name} - {p.Model}"))
                {
                    Console.WriteLine(menuLine);
                }
                Console.WriteLine("0:Exit");
                
                int input = GetInput(0, i - 1);

                switch (input)
                {
                    case 1: CreatePrinter(); break;
                    case 0: return;
                    default:
                        Print(PrinterManager.Printers[input - 2]);
                        break;
                }
            }
        }

        private static void Print(Printer printer)
        {
            PrinterManager.Print(printer);
        }
        
        private static void CreatePrinter()
        {
            Console.WriteLine("Enter name.");
            string name = Console.ReadLine();
            switch (name)
            {
                case "Epson":
                    PrinterManager.Add(ReadModel(), new EpsonFactory()); break;
                case "Canon":
                    PrinterManager.Add(ReadModel(), new CanonFactory()); break;
                default:
                    Console.WriteLine($"Unknown type of printer, try another."); break;
            }

            string ReadModel()
            {
                Console.WriteLine("Enter model.");
                return Console.ReadLine();
            }
        }

        private static int GetInput(int lowIndex, int highIndex)
        {
            Console.WriteLine("Select your choice:");

            while (true)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out int result))
                {
                    if (result <= highIndex && result >= lowIndex)
                        return result;
                }

                Console.WriteLine("Wrong input!");
            }
            
        }
    }
}
