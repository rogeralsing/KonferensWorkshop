using System;
using Akka.Actor;

namespace ConcurrencyExample2
{
    public class PostPersonActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            var person = (Person) message;
            var repo = new PersonRepository();
            if (!repo.Exists(person.Name))
            {
                Console.WriteLine("Person with name {0} was added", person.Name);
                repo.Add(person);
            }
            else
            {
                Console.WriteLine("Person with name {0} already exists", person.Name);
            }
            Sender.Tell("OK");
        }
    }
}
