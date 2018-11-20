using System;

namespace No8.Solution.Logger
{
    public class NLogLogger : ILogger
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private Lazy<NLogLogger> LazyNLogLogger = new Lazy<NLogLogger>(() => new NLogLogger());

        public NLogLogger Instance => LazyNLogLogger.Value;

        public void Fatal(Exception ex)
        {
            if (ex == null)
                throw new ArgumentNullException(nameof(ex));

            logger.Fatal(ex);
        }

        public void Info(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg))
                throw new ArgumentNullException(nameof(msg));

            logger.Info(msg);
        }

        public void Warning(string msg, Exception ex)
        {
            if (string.IsNullOrWhiteSpace(msg))
                throw new ArgumentNullException(nameof(msg));

            if (ex == null)
                throw new ArgumentNullException(nameof(ex));

            logger.Warn(ex, msg);
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
