using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WithJwt.Core.Entites;

namespace WithJwt.Repository.Contexts
{
    public class JwtCourseDbContext : IdentityDbContext<AppUser, AppRole, int>
    {

        public DbSet<Post> Posts { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-1GS5KNR\SQLEXPRESS;Database=JwtCourseDb;Trusted_Connection=True;");
        }

    }
}
