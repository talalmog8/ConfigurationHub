using Configuration.Domain;
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
        public DbSet<Domain.System> ConfigSystems { get; set; }
    }
}