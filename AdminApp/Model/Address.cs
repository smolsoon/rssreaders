using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminApp.Model
{
    public class Address
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Link { get; set; }
        public string Category { get; set; }
    }
}
