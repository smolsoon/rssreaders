using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace RssReaders.Infrastructure.Settings
{
    public static class JWTSecurityKey
    {
        public static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}