using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirestoreEmber;

namespace EmberTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            EmberDatabase database = new EmberDatabase("firemine-8326d",
                "C:\\Database\\FireMine-7b7740133042.json");

            /*var data = new Dictionary<string, object>();
            data["Strawberries"] = 5000;
            data["Blueberries"] = "No";

            await database.CreateDocument("Fruits", "Berries", data);*/
            var data = await database.ReadDocuments("Fruits");
            Console.WriteLine(data[0]);
        }
    }
}
