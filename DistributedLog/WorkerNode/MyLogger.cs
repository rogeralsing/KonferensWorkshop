using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Event;

namespace WorkerNode
{
    public class MyLogger : ReceiveActor
    {
        private void Log(LogEvent message)
        {
            Console.WriteLine("Log: {0}",message);
        }

        public MyLogger()
        {
            Receive<LogEvent>(m => Log(m));           
            Receive<InitializeLogger>(m =>
            {
                Console.WriteLine("Starting MyLogger...");
                Sender.Tell(new LoggerInitialized());
            });
        }
    }
}
