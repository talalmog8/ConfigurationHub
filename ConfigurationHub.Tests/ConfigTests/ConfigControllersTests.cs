using System.Text.Json;
using Configuration.Data;
using ConfigurationHub.Domain;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationHub.Tests.ConfigTests
{
    public class ConfigControllersTests
    {
        protected DbContextOptions<ConfigurationContext> ContextOptions { get; }

        protected ConfigControllersTests(DbContextOptions<ConfigurationContext> contextOptions)
        {
            ContextOptions = contextOptions;
            Seed();
        }

        private void Seed()
        {
            using (var context = new ConfigurationContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var author = new ConfigAuthor()
                {
                    FirstName = "Tal",
                    LastName = "Almog"
                };
                
                var content = new ConfigContent()
                {
                    Content = JsonSerializer.Serialize(new { Port = 200 })
                };

                var system = new Domain.System()
                {
                    MicroserviceName = "ConfigurationHub"
                };

                var config = new Config()
                {
                    ConfigContent = content,
                    Author = author,
                    System = system

                };

                context.Add(config);
                context.SaveChanges();
            }
        }
    }
}
