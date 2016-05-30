using MCO.Applications.WebDispatchPerformance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCO.Applications.WebDispatchPerformance
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            CompositionRoot.Wire(new ApplicationModule());

            var app = CompositionRoot.Resolve<IApp>();

            app.Run();
        }
    }
}
