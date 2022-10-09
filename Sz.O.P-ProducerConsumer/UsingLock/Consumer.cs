using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingLock
{
    public class Consumer
    {
        // a feladattól függően változhatnak a fogyasztó paraméterei
        public Consumer()
        {

        }
        public void Working()
        {
            // egy fogyasztó akkor áll le, ha már nem dolgoznak termelők,
            // és el is fogyott a listából az erőforrás (termék)
            while (Manager.producersWorking || Manager.resources.Count > 0)
            {
                Resource r;
                if (Manager.GetResource(out r))
                {
                    // r lementése / feldolgozása
                }
            }
        }
    }
}
