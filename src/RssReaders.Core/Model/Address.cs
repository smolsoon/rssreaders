using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RssReaders.Core.Model
{
    public class Address
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Link { get; set; }
        public string Category { get; set; }
        public Address(ObjectId id, string link, string category)
        {
            Id = id;
            Link = link;
            Category = category;            
        }
    }
}