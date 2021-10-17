using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
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

            Land[,] world = new Land[25, 25];

            ProcessWorld(world, 10, 15, 15, 25);

            PrintWorld(world, 10, 15, 15, 25);

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static void ProcessWorld(Land[,] world, int startX, int stopX, int startY, int stopY)
        {
            for (int y = startY; y < world.GetLength(1) && y < stopY; y++)
            {
                for (int x = startX; x < world.GetLength(0) && x < stopX; x++)
                {
                    world[x, y] = GetLandInfo(x, y);
                }
            }
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

                    if (world[x, y]?.ForSale ?? false)
                    {
                        Console.Write($" -> {world[x, y].SalesPrice}".PadRight(8));
                    }
                    else
                    {
                        Console.Write("\t");
                    }

                    if (world[x, y]?.RecentlySold ?? false)
                    {
                        Console.Write($" [{string.Join(',', world[x, y].RecentlySoldPrices)}]".PadRight(8));
                    }
                    else
                    {
                        Console.Write("\t");
                    }

                    Console.Write("\t|");
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
                Console.Write(string.Concat(Enumerable.Repeat("-", ((stopX - startX) * 24) - 1)));
                Console.WriteLine("|");
            }
        }

        private static Land GetLandInfo(int x, int y)
        {
            var land = new Land
            {
                X = x,
                Y = y
            };

            GetCnftInfo(land);
            GetCnftAnalyticsInfo(land);

            return land;
        }

        private static void GetCnftAnalyticsInfo(Land land)
        {
            var client = new RestClient($"https://cnftanalytics.io/fphp/search_results.php/?search=pavia^$^{land.X}%20{land.Y}&filter=1&page=1&offset=-120");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Accept-Language", "nl-NL,nl;q=0.9,en-US;q=0.8,en;q=0.7");
            request.AddHeader("Referer", "https://cnftanalytics.io/php/legacySearch.php");
            request.AddHeader("Sec-Fetch-Dest", "empty");
            request.AddHeader("Sec-Fetch-Mode", "cors");
            request.AddHeader("Sec-Fetch-Site", "same-origin");
            request.AddHeader("Sec-GPC", "1");
            request.AddHeader("Cookie", "Theme=dark");
            IRestResponse response = client.Execute(request);

            if (!string.IsNullOrEmpty(response.Content))
            {
                //Console.WriteLine(response.Content);
                var content = response.Content;

                // fix closing tag
                if (content.EndsWith("</"))
                {
                    content += "tr>";
                }

                // fix missing space
                if (content.Contains("\"target"))
                {
                    content = content.Replace("\"target", "\" target");
                }

                // enclose in root node
                content = "<root>" + content + "</root>";

                System.Xml.XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);

                // foreach tr
                var trNodes = doc.DocumentElement.SelectNodes("tr");

                var soldPricesList = new List<decimal>();
                foreach (XmlNode trNode in trNodes)
                {
                    var tdNodes = trNode.SelectNodes("td");
                    var price = tdNodes[2].InnerText;
                    if (decimal.TryParse(price, out decimal parsedPrice))
                    {
                        soldPricesList.Add(parsedPrice);
                    }
                }

                land.RecentlySoldPrices.AddRange(soldPricesList);
            }
        }

        private static void GetCnftInfo(Land land)
        {
            // CNFT.io
            var client = new RestClient("https://api.cnft.io/market/listings");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("search", $"paviaplus{land.X}plus{land.Y}");
            request.AddParameter("sort", "date");
            request.AddParameter("order", "desc");
            request.AddParameter("page", "1");
            request.AddParameter("verified", "true");
            IRestResponse response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<MarketplaceSearchResult>(response.Content);

            if (result.Found == 1)
            {
                land.ForSale = !result.Assets[0].Sold;
                land.SalesPrice = result.Assets[0].Price / 1000000;
            }
        }
    }
}
