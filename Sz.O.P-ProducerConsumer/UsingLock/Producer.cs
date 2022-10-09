using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingLock
{
    public class Producer
    {
        // a termelők megkülönböztetésére
        private static int ID = 0;

        // paraméterbe bekérem, hogy a termelő hány terméket is termeljen
        public Producer(int NumberOfResources)
        {
            numberOfResources = NumberOfResources;
            id = ID++;
        }
        int numberOfResources;
        // jelenleg termelt termék száma
        int currentResources = 0;
        int id;
        // dolgozik-e még a jelenlegi termelő
        public bool IsWorking = true;
        public void Working()
        {
            Resource r = null;
            // a termelő addig dolgozik, amennyi terméket kell csinálnia
            while (currentResources < numberOfResources)
            {
                if (r == null)
                {
                    // ha nincs jelenlegi terméke, akkor csinál egyet
                    r = new Resource(id.ToString());
                }
                // megpróbál elrakni egy erőforrást (terméket), ha nem sikerül, akkor nem teszi null-ra, nem
                // növeli meg a termelt termékek számát
                if (Manager.AddResource(r))
                {
                    r = null;
                    currentResources++;
                }
            }
            // ha végzett a dolgával, akkor hamisra állítja hogy dolgozik
            IsWorking = false;
            // és majd a menedzserben ezt meghívja
            Manager.CheckProducersWorking();
        }
    }
}
