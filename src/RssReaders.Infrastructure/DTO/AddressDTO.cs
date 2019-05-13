using System;
using MongoDB.Bson;

namespace RssReaders.Infrastructure.DTO
{
    public class AddressDTO
    {
        public ObjectId _id { get; set; }
        public string Link { get; set; }
        public string Category { get; set; }
    }
}