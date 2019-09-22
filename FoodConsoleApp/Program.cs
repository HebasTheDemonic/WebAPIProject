using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FoodConsoleApp
{
    class Program
    {
        private const string URL = "http://localhost:52550/api/food";

        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responseMessage = client.GetAsync("").Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string food = responseMessage.Content.ReadAsStringAsync().Result;
                Console.Write("{0} ", food);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)responseMessage.StatusCode, responseMessage.ReasonPhrase);
            }

            Task task = Task.Run(() => Post());
            task.Wait();

            Console.Read();
        }

        private static async void Post()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            Uri uri = new Uri(URL);
            HttpResponseMessage responseMessage;

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string body = "{\"Name\": \"Lazangia\",\"Calories\": 1500,\"Ingridients\": \"Pasta Meat Cheese\",\"Grade\": 10}";
            HttpContent httpContent = new StringContent(body, Encoding.UTF8, "application/json");
            responseMessage = await client.PostAsync(uri, httpContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                Console.Write(responseMessage.StatusCode.ToString());
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)responseMessage.StatusCode, responseMessage.ReasonPhrase);
            }
        }
    }
}
