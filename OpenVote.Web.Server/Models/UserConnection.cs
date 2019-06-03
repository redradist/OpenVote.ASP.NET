using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace OpenVote.Web.Server.Models
{
    public class UserConnection
    {
        public byte[] PublicKey { get; set; }

        public string IpAddress { get; set; }
        public int Port { get; set; }
    }
}
