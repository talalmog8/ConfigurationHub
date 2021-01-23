using System.Text.Json;
using Configuration.Data;
using ConfigurationHub.Data.Repositories;
using ConfigurationHub.Domain;
using ConfigurationHub.Domain.Auth;
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

                var author = new User
                {
                    FirstName = "Tal",
                    LastName = "Almog",
                    Email = "abc",
                    Username = "tal"
                };

                author = new UserService(context).Register(author, "test-password");
                
                var content = new ConfigContent
                {
                    Content = JsonSerializer.Serialize(new { Port = 200 })
                };

                var system = new Microservice
                {
                    Name = "ConfigurationHub"
                };

                var config = new Config
                {
                    ConfigContent = content,
                    Author = author,
                    Microservice = system

                };

                context.Add(config);
                context.SaveChanges();
            }
        }
    }
}
