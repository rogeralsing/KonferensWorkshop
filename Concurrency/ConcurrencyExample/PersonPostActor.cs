using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace ConcurrencyExample2
{
    public class PersonPostActor : ReceiveActor
    {
        public PersonPostActor()
        {
            Receive<Person>(person =>
            {
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
            });
        }
    }
}
