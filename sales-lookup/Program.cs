using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Newtonsoft.Json;
using RestSharp;
using sales_lookup.Models;
using sales_lookup.Models.CNFT.io;

namespace sales_lookup
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting prices");

            Land[,] world = new Land[24, 24];

            var coords = GetSurroundingCoordinates(12, 20);
            var lands = new List<Land>();

            //foreach (var c in coords)
            //{
            //    lands.Add(FindLandSale(c.X, c.Y));
            //}

            //PrintLands(lands);

            PrintWorld(world, 8, 16, 16, 24);

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static void PrintWorld(Land[,] world, int startX, int stopX, int startY, int stopY)
        {
            drawHorizontalLine();

            for (int y = startY; y < world.GetLength(1) && y < stopY; y++)                
            {
                Console.Write("|");

                for (int x = startX; x < world.GetLength(0) && x < stopX; x++)
                {
                    Console.Write($" {x} {y}");

                    Console.Write("\t\t|");
                }

                if (y < world.GetLength(1) - 1)
                {
                    drawHorizontalLine();
                }
            }

            drawHorizontalLine();

            void drawHorizontalLine()
            {
                Console.WriteLine();
                Console.Write("|");
                Console.Write(string.Concat(Enumerable.Repeat("-", ((stopX - startX) * 16) - 1)));
                Console.WriteLine("|");
            }
        }

        private static void PrintLands(List<Land> lands)
        {
            //foreach(var land in lands)
            //{
            //    Console.WriteLine(land);
            //}

            var list = lands.OrderBy(a => a.X).ThenBy(a => a.Y);

            for (int i = list.First().X; i <= list.Select(a => a.X).Max(); i++)
            {
                Console.Write("|\t");

                Console.Write(i);

                if (i == list.Select(a => a.X).Max())
                {
                    Console.WriteLine("|");
                }
            }
        }

        private static List<(int X, int Y)> GetSurroundingCoordinates(int x, int y)
        {
            List<(int, int)> list = new List<(int, int)>();

            list.Add((x - 1, y - 1));
            list.Add((x, y - 1));
            list.Add((x + 1, y - 1));

            list.Add((x - 1, y));
            list.Add((x, y));
            list.Add((x + 1, y));

            list.Add((x - 1, y + 1));
            list.Add((x, y + 1));
            list.Add((x + 1, y + 1));

            return list;
        }

        private static Land FindLandSale(int x, int y)
        {
            // CNFT.io
            //var client = new RestClient("https://api.cnft.io/market/listings");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.POST);
            //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            //request.AddParameter("search", $"paviaplus{x}plus{y}");
            //request.AddParameter("sort", "date");
            //request.AddParameter("order", "desc");
            //request.AddParameter("page", "1");
            //request.AddParameter("verified", "true");
            //IRestResponse response = client.Execute(request);

            //var result = JsonConvert.DeserializeObject<MarketplaceSearchResult>(response.Content);

            //if (result.Found == 1)
            //{
            //    return new Land
            //    {
            //        X = x,
            //        Y = y,
            //        ForSale = !result.Assets[0].Sold,
            //        SalesPrice = result.Assets[0].Price
            //    };
            //}

            return new Land
            {
                X = x,
                Y = y,
                ForSale = false
            };
        }
    }
}
