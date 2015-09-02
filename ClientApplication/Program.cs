using System;
using System.Threading.Tasks;
using RestSharp;

namespace ClientApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //simulera att 10 samtidiga användare/klienter försöker skicka samma data samtidigt
            for (int i = 0; i < 10; i++)
            {
                Task.Factory.StartNew(PostPerson);
            }
            Console.ReadLine();
        }

        private static void PostPerson()
        {
            var client = new RestClient("http://localhost:1234");
            var request = new RestRequest(Method.POST)
            {
                Resource = "/",
                RequestFormat = DataFormat.Json
            };
            
            request.AddBody(new { Name = "Connor MacLeod" });

            var response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
}
