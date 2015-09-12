using System;
using Akka.Actor;

namespace WorkerNode
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("MyCluster"))
            {
                var worker = system.ActorOf<WorkerActor>();

                while(true)
                {
                    Console.WriteLine("Press enter to send work to worker actor..");
                    Console.ReadLine();

                    worker.Tell(new DoWork());
                }
                
            }
        }
    }
}
