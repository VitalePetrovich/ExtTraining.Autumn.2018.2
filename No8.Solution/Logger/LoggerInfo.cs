using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No8.Solution.Logger
{
    public class LoggerInfoEventArgs : EventArgs
    {
        public string Message { get; set; }
        public string PrinterName { get; set; }
        public string PrinterModel { get; set; }
        public string File { get; set; }
        public DateTime Time { get; set; }
    }
}
