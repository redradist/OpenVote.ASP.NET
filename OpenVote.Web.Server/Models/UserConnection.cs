namespace OpenVote.Web.Server.Models
{
    public class UserConnection
    {
        public byte[] PublicKey { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
    }
}
