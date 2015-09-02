using System;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace ClientApplication
{
    class Program
    {
        static void Main(string[] args)
        {
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

            request.AddBody(new { Name = "Olle"});

            var response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
}
