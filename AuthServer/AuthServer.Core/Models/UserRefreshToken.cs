namespace AuthServer.Core.Models
{
    public class UserRefreshToken
    {
        public int UserId { get; set; }
        public string Code { get; set; }
        public DateTime Expiration { get; set; }
    }
}
