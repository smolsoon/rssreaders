using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RssReaders.Core.Model
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; protected set; }
        public string Username { get; protected set; }
        public string Role { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }

        public User (ObjectId id, string role, string username, string email, string password) {
            Id = id;
            Role = role;
            Username = username;
            Email = email;
            Password = password;
        }
    }
}