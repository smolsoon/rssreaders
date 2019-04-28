namespace RssReaders.Infrastructure.Settings
{
    public class JWTSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public int ExpiryMinutes { get; set; }
    }
}