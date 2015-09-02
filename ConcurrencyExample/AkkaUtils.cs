using Akka.Actor;

namespace ConcurrencyExample2
{
    public static class AkkaUtils
    {
        public static readonly ActorSystem sys;
        public static readonly IActorRef PostPerson;

        static AkkaUtils()
        {
            sys = ActorSystem.Create("MySystem");
            PostPerson = sys.ActorOf<PostPersonActor>();
        }
    }
}
