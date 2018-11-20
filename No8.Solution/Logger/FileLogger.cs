using System;
using System.IO;

namespace No8.Solution.Logger
{
    public class FileLogger : ILogger
    {
        public FileLogger(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));

            _fileName = fileName;
        }

        public FileLogger() : this(AppDomain.CurrentDomain.BaseDirectory + "FileLog.txt") { }

        private readonly string _fileName;

        public void Fatal(Exception ex)
        {
            if (ex == null) 
                throw new ArgumentNullException(nameof(ex));

            using (var fs = File.AppendText(_fileName))
            {
                fs.WriteLine($"[{DateTime.Now}] :: FATAL :: {ex.Message}");
            }
        }

        public void Info(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg))
                throw new ArgumentNullException(nameof(msg));

            using (var fs = File.AppendText(_fileName))
            {
                fs.WriteLine($"[{DateTime.Now}] :: INFO :: {msg}");
            }
        }

        public void Warning(string msg, Exception ex)
        {
            if (string.IsNullOrWhiteSpace(msg))
                throw new ArgumentNullException(nameof(msg));

            if (ex == null)
                throw new ArgumentNullException(nameof(ex));

            using (var fs = File.AppendText(_fileName))
            {
                fs.WriteLine($"[{DateTime.Now}] :: WARNING :: {msg} ({ex.Message})");
            }
        }

        public void Register(PrinterManager printerManager)
        {
            if (printerManager == null)
                throw new ArgumentNullException(nameof(printerManager));

            printerManager.StartPrinting += HandleStartPrinting;
            printerManager.EndPrinting += HandleEndPrinting;
            printerManager.AddPrinter += HandleAddPrinting;
            printerManager.RemovePrinter += HandleRemovePrinting;
        }

        public void Unregister(PrinterManager printerManager)
        {
            if (printerManager == null)
                throw new ArgumentNullException(nameof(printerManager));

            printerManager.StartPrinting -= HandleStartPrinting;
            printerManager.EndPrinting -= HandleEndPrinting;
            printerManager.AddPrinter -= HandleAddPrinting;
            printerManager.RemovePrinter -= HandleRemovePrinting;
        }

        public void HandleStartPrinting(object sender, LoggerInfoEventArgs inf)
        {
            if (inf == null)
                throw new ArgumentNullException(nameof(inf));

            Info($"{inf.PrinterName} - {inf.PrinterModel} start printing file: {inf.File}");
        }

        private void HandleEndPrinting(object sender, LoggerInfoEventArgs inf)
        {
            if (inf == null)
                throw new ArgumentNullException(nameof(inf));

            Info($"{inf.PrinterName} - {inf.PrinterModel} end printing file: {inf.File}");
        }

        private void HandleAddPrinting(object sender, LoggerInfoEventArgs inf)
        {
            if (inf == null)
                throw new ArgumentNullException(nameof(inf));

            Info($"Add new printer: {inf.PrinterName} - {inf.PrinterModel}");
        }

        private void HandleRemovePrinting(object sender, LoggerInfoEventArgs inf)
        {
            if (inf == null)
                throw new ArgumentNullException(nameof(inf));

            Info($"Add remove printer: {inf.PrinterName} - {inf.PrinterModel}");
        }
    }
}
