using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No8.Solution.Factories
{
    public class CanonFactory : PrinterFactory
    {
        private static readonly Lazy<CanonFactory> LazyCanonFactory = new Lazy<CanonFactory>(() => new CanonFactory());

        public CanonFactory Instanse => LazyCanonFactory.Value;
        
        public override Printer GetNewPrinter(string model) => new CanonPrinter(model);
    }
}
