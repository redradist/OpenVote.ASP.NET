
using Microsoft.EntityFrameworkCore;
using OpenVote.Web.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenVote.Web.Server.Database
{
    public class DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserConnection> UserConnections { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=openvote;Username=postgres;Password=postgres");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(c => new { c.PublicKey });
            modelBuilder.Entity<UserConnection>().HasKey(c => new { c.PublicKey });
        }
    }
}
