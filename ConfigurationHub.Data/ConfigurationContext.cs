using ConfigurationHub.Domain;
using ConfigurationHub.Domain.Auth;
using Microsoft.EntityFrameworkCore;

namespace Configuration.Data
{
    public sealed class ConfigurationContext : DbContext
    {
        public ConfigurationContext(DbContextOptions<ConfigurationContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        
        public DbSet<Config> Configs { get; set; }
        public DbSet<ConfigContent> ConfigContents { get; set; }
        public DbSet<Microservice> ConfigSystems { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Config>(entity =>
            {
                entity.HasIndex(c => c.LastModified);
                entity.Property(c => c.LastModified).IsRequired();
            });

            builder.Entity<Microservice>(entity =>
            {
                entity.HasIndex(s => s.Name);
                entity.Property(s => s.Name).IsRequired();               
            });

            builder.Entity<User>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Username).IsRequired();
                e.HasIndex(x => x.Username);
                e.HasAlternateKey(t => t.Username);
            });
        }
    }
}