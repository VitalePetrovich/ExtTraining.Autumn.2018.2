using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No8.Solution.Factories
{
    public class EpsonFactory : PrinterFactory
    {
        private static readonly Lazy<EpsonFactory> LazyEpsonFactory = new Lazy<EpsonFactory>(() => new EpsonFactory());

        public EpsonFactory Instance => LazyEpsonFactory.Value;

        public override Printer GetNewPrinter(string model)
            => string.IsNullOrWhiteSpace(model)?throw new ArgumentNullException(nameof(model)):new EpsonPrinter(model);
    }
}
