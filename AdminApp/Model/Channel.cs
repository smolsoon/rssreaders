using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminApp.Model
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


    }
}
