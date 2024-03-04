using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Car;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CarConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            car();
            Console.WriteLine("Car name: ");
            Console.ReadLine();
        }

        private static async Task car()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://vpic.nhtsa.dot.gov/api/vehicles/getallmakes?format=json");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            //Console.WriteLine(await response.Content.ReadAsStringAsync()); // Ez íratná li az egész API server adatait 
            var jsonString = await response.Content.ReadAsStringAsync();
            // Deszerializálja a JSON-választ  => // sorba rendezés.
            var jsonObject = JObject.Parse(jsonString);
            var results = jsonObject["Results"];

            // Név alapján van a szűrés
            foreach (var result in results)
            {
                Console.WriteLine(result["Make_Name"]);
            }
        }
    }
}
