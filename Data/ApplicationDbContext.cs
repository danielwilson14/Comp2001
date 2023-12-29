using Microsoft.EntityFrameworkCore;
using Comp2001.Models;

namespace Comp2001.Data
{
    // Represents the database context used by Entity Framework Core.
    public class ApplicationDbContext : DbContext
    {
        // Constructor for options injection.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet properties for each entity type.
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<UserActivity> UserActivity { get; set; }
        public DbSet<UserPreferences> UserPreferences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("CW2");
        }
    }
}
