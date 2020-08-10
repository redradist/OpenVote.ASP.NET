using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenVote.Web.Server.Database;
using OpenVote.Web.Server.Models;
using OpenVote.Web.Server.Utils;

namespace OpenVote.Web.Server.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[action]/{publicKey}")]
        public async Task<bool> RegisterUserAsync(byte[] publicKey)
        {
            var remoteIpAddress = Request.GetIpAddress();
            var remotePort = Request.HttpContext.Connection.RemotePort;
            Console.WriteLine($"remoteIpAddress = {remoteIpAddress}");
            Console.WriteLine($"remotePort = {remotePort}");

            using (DBContext db = new DBContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    User user = new User()
                    {
                        PublicKey = publicKey
                    };
                    UserConnection userConnection = new UserConnection()
                    {
                        PublicKey = publicKey,
                        IpAddress = remoteIpAddress,
                        Port = remotePort,
                    };
                    bool isUserFound = db.Users.Any(u => u.PublicKey == publicKey);
                    if (!isUserFound)
                    {
                        await db.Users.AddAsync(user);
                        await db.UserConnections.AddAsync(userConnection);
                    }
                    else
                    {
                        db.UserConnections.Update(userConnection);
                    }
                    _ = await db.SaveChangesAsync();
                }

                var users = db.Users.ToList();
                Console.WriteLine("Users list:");
                foreach (User u in users)
                {
                    Console.WriteLine($"PublicKey = {u.PublicKey}, Name = {u.Name}, Age = {u.Age}");
                }

                var usersInfo = db.UserConnections.ToList();
                Console.WriteLine("Users list:");
                foreach (UserConnection u in usersInfo)
                {
                    Console.WriteLine($"PublicKey = {u.PublicKey}, Address = {u.IpAddress}:{u.Port}");
                }
            }
            return false;
        }

        [HttpGet("[action]")]
        public bool CheckInUser()
        {
            return false;
        }
    }
}