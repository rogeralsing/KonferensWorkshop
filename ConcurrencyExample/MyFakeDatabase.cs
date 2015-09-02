using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace ConcurrencyExample2
{
    public static class MyFakeDatabase
    {
        //trådsäker simulering av databastabell
        private static readonly ConcurrentDictionary<int,Person> PersonTable  = new ConcurrentDictionary<int,Person> ();
        
        //räknare för att simulera databas auto inc / identity field
        private static int _autoId;

        public static Person[] Persons
        {
            get
            {
                //simulera latency i databasanrop
                Thread.Sleep(1500);
                return PersonTable.Values.ToArray();    
            }            
        }

        public static void Insert(Person person)
        {
            Interlocked.Increment(ref _autoId);
            person.Id = _autoId;
            PersonTable.TryAdd(person.Id, person);
        }
    }
}