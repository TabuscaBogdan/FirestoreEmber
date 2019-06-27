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

            
            var data1 = new Dictionary<string, object>();
            data1["Daisy"] = 500;
            data1["Bloom"] = 38;
            data1["Sunflower"] = 50;
            
            var data0 =new Dictionary<string,object>();
            data0["Nightshade"] = 10;

            var data2 = new Dictionary<string, object>();
            data2["Carrak"] = 5;
            data2["Sailboat"] = 20;
            data2["Battleship"] = 0;

            

            var col1 = new Dictionary<string,Dictionary<string,object>>();
            col1["Flowers"] = data1;
            col1["Bushes"] = data0;

            var col2 = new  Dictionary<string,Dictionary<string,object>>();
            col2["Ships"] = data2;

            var bigBatch = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>();
            bigBatch["Boats"] = col2;
            bigBatch["Plants"] = col1;

            await database.CreateDocumentBatch(bigBatch);


            //var data = await database.ReadDocuments("Fruits");
            //Console.WriteLine(data[0]);
        }
    }
}
