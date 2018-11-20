using System;
using System.Linq;
using No8.Solution.Factories;
using No8.Solution.Logger;

namespace No8.Solution.Console
{
    using Console = System.Console;
    
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            PrinterManager printerManager = new PrinterManager(new FileLogger());
            ShowMenu(printerManager);
        }

        private static void ShowMenu(PrinterManager printerManager)
        {
            if (printerManager == null)
                throw new ArgumentNullException(nameof(printerManager));

            while (true)
            {
                Console.Clear();

                int i = 2;
                Console.WriteLine("1:Add new printer");
                foreach (var menuLine in printerManager.Printers.Select(p => $"{i++}:Print on {p.Name} - {p.Model}"))
                {
                    Console.WriteLine(menuLine);
                }
                Console.WriteLine("0:Exit");
                
                int input = GetInput(0, i - 1);

                switch (input)
                {
                    case 1: CreatePrinter(printerManager); break;
                    case 0: return;
                    default:
                        printerManager.Print(printerManager.Printers[input - 2]);
                        Console.ReadKey();
                        break;
                }
            }
        }
        
        private static void CreatePrinter(PrinterManager printerManager)
        {
            if (printerManager == null)
                throw new ArgumentNullException(nameof(printerManager));

            Console.WriteLine("Enter name.");
            string name = Console.ReadLine();
            switch (name)
            {
                case "Epson":
                    printerManager.Add(ReadModel(), new EpsonFactory()); break;
                case "Canon":
                    printerManager.Add(ReadModel(), new CanonFactory()); break;
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
