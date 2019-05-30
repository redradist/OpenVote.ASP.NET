using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace OpenVote.Web.Server.Models
{
    public class UserInfo
    {
        public int RandomId { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public byte[] PublicKey { get; set; }
    }
}
