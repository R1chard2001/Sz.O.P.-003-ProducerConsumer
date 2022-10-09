using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace UsingMonitor
{
    public static class Manager
    {
        // termelő lista, könnyű kezelhetőség érdekében
        static List<Producer> producers = new List<Producer>();
        // fogyasztó lista, könnyű kezelhetőség érdekében
        static List<Consumer> consumers = new List<Consumer>();
        // erőforrás (termék) lista, ez lesz a közös erőforrásuk a termelőknek és fogyasztóknak,
        // ezt kell majd levédeni
        public static List<Resource> resources = new List<Resource>();
        // az erőforráslista méreti limitje
        static int resourceLimit = 10;
        // a termelők, hogy van-e közöttük, aki dolgozik
        static public bool producersWorking = true;

        // egy erőforrás hozzáadása a listához
        public static bool AddResource(Resource r)
        {
            // sikeresen elhelyezett-e egyet
            bool success = false;
            // a lista lefoglalása
            Monitor.Enter(resources);
            while (resources.Count == resourceLimit)
            {
                // a lista elengedése, és várakozás egy Pulse-ra
                Monitor.Wait(resources);
            }
            if (resources.Count < resourceLimit)
            {
                Console.WriteLine(" + Resource added:\n{0}\n",r);
                resources.Add(r);
                success = true;
            }
            // mindenki felébresztése
            Monitor.PulseAll(resources);
            // erőforrás elengedése
            Monitor.Exit(resources);
            // visszatérés a siker eredményével
            return success;
        }

        public static bool GetResource(out Resource r)
        {
            // itt hasonlóan az AddResource-hoz
            bool success = false;
            r = null;
            Monitor.Enter(resources);
            while (resources.Count == 0 && producersWorking)
            {
                Monitor.Wait(resources);
            }
            if (resources.Count > 0)
            {
                r = resources[0];
                success = true;
                resources.RemoveAt(0);
                Console.WriteLine(" - Resource taken:\n{0}\n", r);
            }
            Monitor.PulseAll(resources);
            Monitor.Exit(resources);
            return success;
        }
        // megnézi, hogy van-e még termelő, aki dolgozik, ha nincs, akkor hamisra
        // állítja a producersWorking értékét
        public static void CheckProducersWorking()
        {
            foreach (Producer p in producers)
            {
                if (p.IsWorking)
                {
                    return;
                }
            }
            producersWorking = false;
        }

        // termelő hozzáadása
        public static void AddProducer(Producer p)
        {
            producers.Add(p);
        }
        //fogyasztó hozzáadása
        public static void AddConsumer(Consumer c)
        {
            consumers.Add(c);
        }
        // a szálak elindítása
        public static void StartWorking()
        {
            // egyes feladatokban érdemes lehet lementeni a szálakat
            //  - egyik módszer lehet maga a termelők és fogyasztók osztályának
            // konstruktorában külön példányszintű változóba lementeni
            //  - másik módszer lehet itt a Manager osztályban egy Thread listába lementeni hozzáadáskor
            // a megfelelő paraméterrel 
            foreach (Producer p in producers)
            {
                new Thread(p.Working).Start();
            }
            foreach (Consumer c in consumers)
            {
                new Thread(c.Working).Start();
            }
        }
    }
}
