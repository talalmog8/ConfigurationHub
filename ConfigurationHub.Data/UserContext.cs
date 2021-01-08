using Configuration.Data;
using ConfigurationHub.Domain.Auth;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationHub.Data
{
    public sealed class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Username).IsRequired();
                e.HasIndex(x => x.Username);
                e.HasAlternateKey(t => t.Username);
            });
        }
    }
}
