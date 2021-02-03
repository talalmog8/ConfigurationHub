using ConfigurationHub.Domain;
using ConfigurationHub.Domain.Auth;
using ConfigurationHub.Domain.ConfigModels;
using ConfigurationHub.Domain.ConfigModels.Content;
using ConfigurationHub.Domain.ConfigModels.MicroserviceModels;
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
        public DbSet<Microservice> MicroServices { get; set; }
        public DbSet<ConfigurationHub.Domain.ConfigModels.SystemModels.System> Systems { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) { }
    }
}