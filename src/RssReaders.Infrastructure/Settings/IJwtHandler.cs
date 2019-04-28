using System;
using RssReaders.Infrastructure.DTO;

namespace RssReaders.Infrastructure.Settings
{
    public interface IJwtHandler
    {
         JwtDTO CreateToken(Guid userId, string role);
    }
}