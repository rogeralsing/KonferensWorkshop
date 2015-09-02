using Akka.Actor;
using Akka.Routing;

namespace ConcurrencyExample2
{
    public static class AkkaUtils
    {
        public static readonly ActorSystem sys;
        public static readonly IActorRef PostPerson;

        static AkkaUtils()
        {
            sys = ActorSystem.Create("MySystem");            
            PostPerson = sys.ActorOf(Props.Create<PostPersonActor>().WithRouter(new ConsistentHashingPool(10,null,null,null)));
        }
    }
}
