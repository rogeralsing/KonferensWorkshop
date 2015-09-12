using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Event;

namespace WorkerNode
{
    public class DoWork
    {
        
    }
    public class WorkerActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();

        public WorkerActor()
        {
            Receive<DoWork>(msg =>
            {
                _log.Info("Doing some work..");
            });
        }
    }
}
