using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace No8.Solution
{
    public delegate void PrinterDelegate(string arg);

    public static class PrinterManager
    {
        static PrinterManager()
        {
            Printers = new List<Printer>();
        }
        
        public static List<Printer> Printers { get; private set; }

        public static void Add(string model, PrinterFactory factory)
        {
            Printer printer = factory.GetNewPrinter(model);

            if (!Printers.Contains(printer))
            {
                Printers.Add(printer);
                Log($"Printer: {printer.Name} - {printer.Model} added.");
            }
        }

        public static void Remove(string name, string model)
        {
            Printers.RemoveAll(p => p.Name == name && p.Model == model);
        }
        
        public static void Print(Printer printer)
        {
            using (var o = new OpenFileDialog())
            {
                o.ShowDialog();

                using (var f = File.OpenRead(o.FileName))
                {
                    Log("Print started");
                    OnStartPrinting($"{DateTime.Now}:: Start printing");
                    printer.Print(f);
                    Log("Print finished");
                    OnEndPringting($"{DateTime.Now}:: End printing");
                }
            }
        }

        private static void Log(string s)
        {
            using (var fs = File.AppendText("log.txt"))
            {
                fs.WriteLine($"{DateTime.Now}:: {s}");
            }
        }

        public static event PrinterDelegate StartPrinting;

        public static event PrinterDelegate EndPrinting;

        private static void OnStartPrinting(string arg)
        {
            StartPrinting?.Invoke(arg);
        }

        private static void OnEndPringting(string arg)
        {
            EndPrinting?.Invoke(arg);
        }
    }
}