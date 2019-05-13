using System;
using MongoDB.Bson;

namespace RssReaders.Infrastructure.Commands
{
    public class CreateAddress
    {
        public ObjectId _id { get; set; }
        public string Link { get; set; }
        public string Category { get; set; }
    }
}