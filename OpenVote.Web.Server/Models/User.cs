using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenVote.Web.Server.Models
{
    public class User
    {
        public byte[] PublicKey { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Courtry { get; set; }
    }
}
