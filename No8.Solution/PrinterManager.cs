using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using No8.Solution.Logger;

namespace No8.Solution
{
    public class PrinterManager
    {
        public PrinterManager(ILogger logger) : this()
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            logger.Register(this);
        }

        public PrinterManager()
        {
            Printers = new List<Printer>();
        }
        
        private readonly ILogger _logger;

        //Свойство, т.к. удобно получать список принтеров извне класса. 
        //Можно использовать закрытое поле и написать функцию, которая возвращает коллекцию принтеров.
        //Не уверен как правильнее, но через свойство проще и короче.
        public List<Printer> Printers { get; private set; }

        public void Add(string model, PrinterFactory factory)
        {
            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentNullException(nameof(model));

            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            Printer printer = factory.GetNewPrinter(model);

            if (!Printers.Contains(printer))
            {
                Printers.Add(printer);
                OnAddPrinter(new LoggerInfoEventArgs()
                                 {
                                     Message = $"Printer: {printer.Name} - {printer.Model} has been added.",
                                     PrinterModel = printer.Model,
                                     PrinterName = printer.Name,
                                     Time = DateTime.Now
                                 });
            }
        }

        public void Remove(string name, string model)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentNullException(nameof(model));

            Printers.RemoveAll(p => p.Name == name && p.Model == model);
            OnRemovePrinter(new LoggerInfoEventArgs()
                                {
                                    Message = $"Printer: {name} - {model} has been deleted.",
                                    PrinterModel = model,
                                    PrinterName = name,
                                    Time = DateTime.Now
                                });
        }
        
        public void Print(Printer printer)
        {
            if (printer == null)
                throw new ArgumentNullException(nameof(printer));

            using (var o = new OpenFileDialog())
            {
                o.ShowDialog();

                using (var f = File.OpenRead(o.FileName))
                {
                    OnStartPrinting(new LoggerInfoEventArgs()
                                        {
                                            Message = "Start printing",
                                            PrinterModel = printer.Model,
                                            PrinterName = printer.Name,
                                            File = o.FileName,
                                            Time = DateTime.Now
                                        } );
                    printer.Print(f);
                    OnEndPringting(new LoggerInfoEventArgs()
                                       {
                                           Message = "End printing",
                                           PrinterModel = printer.Model,
                                           PrinterName = printer.Name,
                                           File = o.FileName,
                                           Time = DateTime.Now
                                        } );
                }
            }
        }
        
        public event EventHandler<LoggerInfoEventArgs> StartPrinting = delegate { };
        
        public event EventHandler<LoggerInfoEventArgs> EndPrinting = delegate { };

        public event EventHandler<LoggerInfoEventArgs> AddPrinter = delegate { };

        public event EventHandler<LoggerInfoEventArgs> RemovePrinter = delegate { };
        
        private void OnStartPrinting(LoggerInfoEventArgs arg)
        {
            if (arg == null)
                throw new ArgumentNullException(nameof(arg));

            StartPrinting(this, arg);
        }

        private void OnEndPringting(LoggerInfoEventArgs arg)
        {
            if (arg == null)
                throw new ArgumentNullException(nameof(arg));

            EndPrinting(this, arg);
        }

        private void OnAddPrinter(LoggerInfoEventArgs arg)
        {
            if (arg == null)
                throw new ArgumentNullException(nameof(arg));

            AddPrinter(this, arg);
        }

        private void OnRemovePrinter(LoggerInfoEventArgs arg)
        {
            if (arg == null)
                throw new ArgumentNullException(nameof(arg));

            RemovePrinter(this, arg);
        }
    }
}