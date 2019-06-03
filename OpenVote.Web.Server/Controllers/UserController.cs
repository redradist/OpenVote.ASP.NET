using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using OpenVote.Web.Server.Database;
using OpenVote.Web.Server.Models;

namespace OpenVote.Web.Server.Controllers
{
    public static class RequestExtention
    {
        public static string GetIpAddress(this HttpRequest request)
        {
            var cfConnectionIpAddess = request.Headers["CF-CONNECTING-IP"];
            if (!StringValues.IsNullOrEmpty(cfConnectionIpAddess))
                return cfConnectionIpAddess;

            var httpXForwardedForIpAddress = request.Headers["HTTP_X_FORWARDED_FOR"];
            if (!StringValues.IsNullOrEmpty(httpXForwardedForIpAddress))
            {
                var addresses = httpXForwardedForIpAddress.ToArray();
                if (addresses.Length != 0)
                    return addresses[0];
            }

            return request.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }

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