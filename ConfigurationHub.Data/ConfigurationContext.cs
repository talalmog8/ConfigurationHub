using ConfigurationHub.Domain;
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
        public DbSet<ConfigAuthor> ConfigAuthors { get; set; }
        public DbSet<ConfigContent> ConfigContents { get; set; }
        public DbSet<ConfigurationHub.Domain.System> ConfigSystems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Config>(entity =>
            {
                entity.HasIndex(c => c.LastModified);
                entity.Property(c => c.LastModified).IsRequired();
            });

            builder.Entity<ConfigAuthor>(entity =>
            {
                entity.HasIndex(a => new { a.FirstName, a.LastName });
                entity.Property(a => a.FirstName).IsRequired();
                entity.Property(a => a.LastName).IsRequired();
            });

            builder.Entity<ConfigurationHub.Domain.System>(entity =>
            {
                entity.HasIndex(s => s.MicroserviceName);
                entity.Property(s => s.MicroserviceName).IsRequired();               
            });
        }
    }
}