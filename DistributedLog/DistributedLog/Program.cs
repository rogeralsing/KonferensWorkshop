using System;
using Akka.Actor;

namespace DistributedLog
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("MyCluster"))
            {
                Console.ReadLine();
            }
        }
    }
}
