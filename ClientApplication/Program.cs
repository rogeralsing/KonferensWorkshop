using System;
using RestSharp;

namespace ClientApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var names = new[] {"Connor MacLeod", "Roger", "Klas", "Anders", "Jerker"};
            //simulera att 10 samtidiga användare/klienter försöker skicka samma data samtidigt
            foreach (var name in names)
            {
                for (int i = 0; i < 10; i++)
                {
                    PostPerson(name);
                }
            }
            Console.ReadLine();
        }

        private static void PostPerson(string name)
        {
            var client = new RestClient("http://localhost:1234");
            var request = new RestRequest(Method.POST)
            {
                Resource = "/",
                RequestFormat = DataFormat.Json
            };
            
            request.AddBody(new { Name = name });

            var response = client.ExecuteTaskAsync(request);
        }
    }
}
