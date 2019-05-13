using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RssReaders.Core.Model
{
    public class Channel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Language { get; set; }
        public string Copyright { get; set; }
        public DateTime LastBuildDate { get; set; }
        public DateTime PubDate { get; set; }
        public ICollection<Item> Items { get; set; }

        public Channel(ObjectId id, string title, string description, string link, string language, string copyright,
            DateTime lastBuildDate, DateTime pubDate, ICollection<Item> items)
        {
            Id = id;
            Title = title;          
            Description = description;
            Link = link;
            Language = language;
            Copyright = copyright;
            LastBuildDate = lastBuildDate;
            PubDate = pubDate;
            Items=  items;  
        }
    }
}