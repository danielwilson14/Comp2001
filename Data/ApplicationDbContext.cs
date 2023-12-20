using Microsoft.EntityFrameworkCore;
using Comp2001.Models;

namespace Comp2001.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public ApplicationDbContext() { }
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<UserActivity> UserActivity { get; set; }
        public DbSet<UserPreferences> UserPreferences { get; set; }

    }
}
