﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
