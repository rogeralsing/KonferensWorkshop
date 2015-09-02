using System;
using System.Linq;
using Akka.Actor;
using Akka.Routing;
using Nancy;
using Nancy.Hosting.Self;
using Nancy.ModelBinding;

namespace ConcurrencyExample2
{

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PersonRepository
    {
        public bool Exists(string name)
        {
            var person = MyFakeDatabase.Persons.FirstOrDefault(p => p.Name == name);
            return person != null;
        }

        public void Add(Person person)
        {
            MyFakeDatabase.Insert(person);
        }

        public Person[] GetAll()
        {
            return MyFakeDatabase.Persons;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new NancyHost(new Uri("http://localhost:1234")))
            {
                host.Start();
                Console.ReadLine();
            }
        }
    }

    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = x =>
            {
                var repo = new PersonRepository();
                var persons = repo.GetAll();
                return Response.AsJson(persons);
            };

            Post["/"] = x =>
            {
                var person = this.Bind<Person>();
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

                return (Response) "OK";
            };

            //Post["/",true] = async (x,ct) =>
            //{
            //    var person = this.Bind<Person>();
            //    var envelope = new ConsistentHashableEnvelope(person, person.Name);
            //    var result = await AkkaUtils.PostPerson.Ask<string>(envelope);
            //    return (Response) result;
            //};
        }
    }
}
