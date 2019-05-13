using System;
using System.Collections.Generic;
using System.Text;

namespace RssParser.RssParserModel.FeedMapping
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime PubDate { get; set; }

        


    }
}
