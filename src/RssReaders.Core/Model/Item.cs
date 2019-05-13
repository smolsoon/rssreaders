using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RssReaders.Core.Model
{
    public class Item
    {
        [BsonId]
        public ObjectId  Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime PubDate { get; set; }

        public Item(ObjectId id, string title, string link, string description, DateTime pubDate)
        {
            Id = id;
            Title = title;
            Link = link;
            Description = description;
            PubDate = pubDate;
        }
    }
}