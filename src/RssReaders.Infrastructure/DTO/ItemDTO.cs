using System;
using MongoDB.Bson;

namespace RssReaders.Infrastructure.DTO
{
    public class ItemDTO
    {
        public ObjectId _id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime PubDate { get; set; }
    }
}