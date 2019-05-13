using System;
using MongoDB.Bson;
using RssReaders.Infrastructure.DTO;

namespace RssReaders.Infrastructure.Settings
{
    public interface IJwtHandler
    {
         JwtDTO CreateToken(ObjectId userId, string role);
    }
}