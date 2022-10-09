using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingMonitor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 4 fő osztály lesz:
            // - Producer: ez lesz a termelők osztálya
            // - Consumer: ez lesz a fogyasztók osztálya
            // - Resource: ez lesz a termelt erőforrás osztálya
            // - Manager: ez fogja kezelni a szálakat, és a közös erőforrásokat
            
            // a termelők hozzáadása
            Manager.AddProducer(new Producer(20));
            Manager.AddProducer(new Producer(10));
            Manager.AddProducer(new Producer(50));

            // a fogyasztók hozzáadása
            Manager.AddConsumer(new Consumer());
            Manager.AddConsumer(new Consumer());

            // a szálak elindítása
            Manager.StartWorking();

            Console.ReadLine();
        }
    }
}
