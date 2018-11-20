using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No8.Solution
{
    public abstract class PrinterFactory
    {
        public abstract Printer GetNewPrinter(string model);
    }
}
