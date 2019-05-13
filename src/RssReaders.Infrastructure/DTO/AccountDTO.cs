using MongoDB.Bson;
using System;

namespace RssReaders.Infrastructure.DTO
{
    public class AccountDTO
    {
        public ObjectId _id { get; protected set; }
        public string Username { get; protected set; }
        public string Role { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
    }
}