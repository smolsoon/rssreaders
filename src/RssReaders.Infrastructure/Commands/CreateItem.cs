using MongoDB.Bson;
using System;

namespace RssReaders.Infrastructure.Commands
{
    public class CreateItem
    {
        public ObjectId _id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime PubDate { get; set; }
    }
}