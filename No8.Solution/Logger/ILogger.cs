using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No8.Solution.Logger
{
    public interface ILogger
    {
        void Info(string msg);

        void Warning(string msg, Exception ex);

        void Fatal(Exception ex);

        void Register(PrinterManager printerManager);

        void Unregister(PrinterManager printerManager);
    }
}
