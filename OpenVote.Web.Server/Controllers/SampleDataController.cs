using OpenVote.Web.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenVote.Web.Server.Database;
using OpenVote.Web.Server.Models;

namespace OpenVote.Web.Server.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();

            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            var remotePort = Request.HttpContext.Connection.RemotePort;
            Console.WriteLine($"remoteIpAddress = {remoteIpAddress}");
            Console.WriteLine($"remotePort = {remotePort}");

            using (ApplicationContext db = new ApplicationContext())
            {
                User user1 = new User { Name = "Tom", Age = 33 };
                User user2 = new User { Name = "Alice", Age = 26 };

                db.Users.Add(user1);
                db.Users.Add(user2);
                db.SaveChanges();

                var users = db.Users.ToList();
                Console.WriteLine("Users list:");
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }

                UserInfo user11 = new UserInfo {
                    RandomId = 1,
                    IpAddress = "asdaasfas",
                    Port = 2234,
                    PublicKey = new byte[] { }
                };
                UserInfo user12 = new UserInfo {
                    RandomId = 2,
                    IpAddress = "sfdfdadsdaasd",
                    Port = 2234,
                    PublicKey = new byte[] { }
                };

                db.UsersInfo.Add(user11);
                db.UsersInfo.Add(user12);
                db.SaveChanges();

                var usersInfo = db.UsersInfo.ToList();
                Console.WriteLine("Users list:");
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }
            }

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }
    }
}
