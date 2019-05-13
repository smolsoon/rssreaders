using System;
using RssParser.RssParserModel;
using RssParser.RssParserModel.FeedMapping;

namespace RssParser
{
    class Program
    {
        static void Main(string[] args)
        {

            string url = @"https://www.rmf24.pl/ekonomia/feed";

            var temp = RssParserModel.RssParser.ParseRss(url);


            foreach (var item in temp)
            {
                Console.WriteLine(item.Title);
                Console.WriteLine(item.Description);
                Console.WriteLine(item.Link);
                Console.WriteLine("---------");

            }



        }
    }
}
