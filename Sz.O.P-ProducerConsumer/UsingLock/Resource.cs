using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingLock
{
    public class Resource
    {
        // egy statikus id, e segítségével lehet megkülönböztetni az erőforrásokat
        // ha a feladat úgy kéri, akkor nem kell ilyet, vagy máshogy oldódik az meg
        private static int ID = 0;

        // a konstruktor paraméterében kérek be egy változót, ez fogja majd megkülönböztetni, hogy melyik
        // termelő termelte
        public Resource(string name)
        {
            this.name = name;
            this.id = ID++;
        }
        string name;
        int id;

        // ToString átírása, ez csak egyszerűsíti a kiíratást
        public override string ToString()
        {
            return String.Format("{0}: {1}", name, id);
        }
    }
}
