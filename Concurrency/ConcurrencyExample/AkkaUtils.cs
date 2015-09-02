using Akka.Actor;
using Akka.Routing;

namespace ConcurrencyExample2
{
    public static class AkkaUtils
    {
        public static readonly IActorRef PostPerson;

        static AkkaUtils()
        {
            var sys = ActorSystem.Create("MySystem");
            PostPerson = sys.ActorOf(Props.Create<PostPersonActor>().WithRouter(new ConsistentHashingPool(10)));
        }
    }
}
