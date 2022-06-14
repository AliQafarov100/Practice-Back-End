using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _6hours_Back_End.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _6hours_Back_End.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<AnotherSetting> AnotherSettings { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
