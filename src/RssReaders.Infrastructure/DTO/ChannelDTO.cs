using System;
using System.Collections.Generic;
using MongoDB.Bson;
using RssReaders.Core.Model;

namespace RssReaders.Infrastructure.DTO
{
    public class ChannelDTO
    {
        public ObjectId _id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Language { get; set; }
        public string Copyright { get; set; }
        public DateTime LastBuildDate { get; set; }
        public DateTime PubDate { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}