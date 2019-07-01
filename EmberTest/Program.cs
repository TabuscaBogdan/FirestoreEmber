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

            
            //batch document creation
            /*
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
            */


            //Add anonymus document
            /*
            var anonymusData = new Dictionary<string, object>();
            anonymusData["Grass"] = "A lot of grass";
            anonymusData["Moss"] = 3000;

            string id = await database.CreateAnonymousDocument("Plants", anonymusData);
            Console.WriteLine(id);
            */

            //Delete fields
            /*
            var fieldList = new List<string>();
            fieldList.Add("StrawberryCream");
            await database.DeleteDocumentFields("Fruits/Berries/Treats", "Creams", fieldList);
            */
            //delete collection
            //var data = await database.ReadDocuments("Fruits");
            /*await database.DeleteCollection("Fruits");
            var docToDel = new Dictionary<string,List<string>>();
            docToDel["users"] = new List<string>();
            docToDel["users"].Add("Timmy");
            docToDel["users"].Add("Jimmy");
            //await database.DeleteDocumentBatch(docToDel);
            */
            
            //update collection
            var otherShips = new Dictionary<string,object>();
            otherShips["Fregate"] = 500;
            otherShips["Line Ship"] = false;

            var otherPlants = new Dictionary<string,object>();
            otherPlants["Acacia"] = "Are semi bushes that grow on arrid conditions";
            otherPlants["Merucia"] = "A type of soft bush";

            var morePlants = new Dictionary<string,object>();
            morePlants["Daybloom"] = " A cute flower the blooms at dawn";
            morePlants["Lily"] = "A flower that made men rich";

            var docUpdateBoats= new Dictionary<string, Dictionary<string,object>>();
            docUpdateBoats["Ships"] = otherShips;

            
            var docUpdatePlants = new Dictionary<string, Dictionary<string,object>>();
            docUpdatePlants["Bushes"] = otherPlants;
            docUpdatePlants["Flowers"] = morePlants;

            var batch = new Dictionary<string,Dictionary<string,Dictionary<string,object>>>();
            batch["Plants"] = docUpdatePlants;
            batch["Boats"] = docUpdateBoats;

            //await database.UpdateDocumentBatch(batch);
            //await database.UpdateDocument("Boats", "Ships", otherShips);
            var timePlants = new Dictionary<string,object>();
            timePlants["Sunflower"] = 80;
            await database.UpdateDocumentWithTimestamp("Plants", "Flowers", timePlants);

            //var data = await database.ReadDocuments("Fruits");
            //Console.WriteLine(data[0]);
        }
    }
}
