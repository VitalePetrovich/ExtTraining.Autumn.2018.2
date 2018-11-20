using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No8.Solution
{
    //Плохо и слаборасширяемо, т.к. при добавлении нового принтера (не Epson и Canon)
    //Придется дописывать фабрику для него и править консоль.
    public abstract class PrinterFactory
    {
        public abstract Printer GetNewPrinter(string model);
    }
}
