using System;
using System.Threading.Tasks;
using Akka.Actor;

namespace CircuitBreaker
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("mysystem"))
            {
                //starta en loop som spammar databasen med massor av frågor
                RunQueries(system);

                while (true)
                {                    
                    var command = Console.ReadKey();

                    if (command.Key == ConsoleKey.UpArrow)
                    {
                        MyDatabase.Up();   
                    }
                    if (command.Key == ConsoleKey.DownArrow)
                    {
                        MyDatabase.Down();
                    }
                }
            }
        }

        static async Task RunQueries(ActorSystem system)
        {
            var db = system.ActorOf<DbActor>();
            while (true)
            {
                var res = await db.Ask(new DbRequest("select * from foo"));
                await Task.Delay(TimeSpan.FromMilliseconds(500));
            }
        }
    }

    public static class MyDatabase
    {
        private static bool IsOpen = true;

        public static string RunSql(string sql)
        {
            if (!IsOpen)
                throw new Exception("DB is in flames, nothing works!");

            return "some result";
        }

        public static void Up()
        {
            IsOpen = true;
        }

        public static void Down()
        {
            IsOpen = false;
        }
    }

    public class DbRequest
    {
        public string SQL { get;private set; }

        //det här vore självklart farligt ur sql injection synpunkt
        //ska detta göras korrekt så skulle även parametrar skickas med här        
        public DbRequest(string sql)
        {
            SQL = sql;
        }
    }

    public class DbResponse
    {
        public string Result { get;private set; }

        public DbResponse(string result)
        {
            Result = result;
        }
    }

    public class DbFailure
    {        
    }

    public class DbPing
    {        
    }

    public class DbActor : ReceiveActor
    {
        public DbActor()
        {
           Become(Open);
        }

        public void Open()
        {
            Receive<DbRequest>(query =>
            {
                try
                {
                    var result = MyDatabase.RunSql(query.SQL);
                    Console.WriteLine("DB is UP, returning response");                    
                    Sender.Tell(new DbResponse(result));
                }
                catch
                {
                    Become(Closed);
                    PingSelf();
                    Sender.Tell(new DbFailure());
                }
            });
        }

        private void PingSelf()
        {
            Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(2), Self, new DbPing(), Self);
        }

        public void Closed()
        {
            Receive<DbPing>(ping =>
            {
                Console.WriteLine("Pinging DB");
                try
                {
                    MyDatabase.RunSql("ping...");
                    Become(Open);
                }
                catch
                {
                    PingSelf();
                }
            });
            Receive<DbRequest>(query =>
            {
                Console.WriteLine("DB is DOWN, returning failure result");
                Sender.Tell(new DbFailure());
            });
        }
    }        
}
