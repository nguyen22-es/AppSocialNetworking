using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ManageAppDbContext:IdentityDbContext<ManagerUser>
    {
        public ManageAppDbContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().Property(x => x.Id).HasMaxLength(50).IsRequired(true);
            builder.Entity<ManagerUser>().Property(x => x.Id).HasMaxLength(50).IsRequired(true);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }


       public DbSet<ManagerUser> ManageUsers { get; set; }
        public DbSet<Friendships>  Friendships { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Posts>  Posts { get; set; }
        public DbSet<Comments> Comments { get; set; }

    }
}
