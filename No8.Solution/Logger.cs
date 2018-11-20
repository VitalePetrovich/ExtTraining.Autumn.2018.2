using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No8.Solution
{
    interface ILogger
    {
        void Info(string msg);

        void Warning(Exception ex);

        void Fail(Exception ex);
    }

    class FileLogger : ILogger
    {
        public void Fail(Exception ex)
        {
            throw new NotImplementedException();
        }

        public void Info(string msg)
        {
            throw new NotImplementedException();
        }

        public void Warning(Exception ex)
        {
            throw new NotImplementedException();
        }

        public static void RegisterLogger(string msg)
        {
            PrinterManager.StartPrinting += this.Info(msg);
            PrinterManager.EndPrinting += this.Info(msg);
        }
    }
}
