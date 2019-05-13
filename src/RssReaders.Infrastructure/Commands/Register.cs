using MongoDB.Bson;
using System;

namespace RssReaders.Infrastructure.Commands
{
    public class Register
    {
        public ObjectId UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}