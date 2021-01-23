using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Configuration.Data;
using ConfigurationHub.Domain.Auth;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationHub.Tests.AuthTests
{
    public class AuthTests
    {
        protected DbContextOptions<ConfigurationContext> ContextOptions { get; }

        protected AuthTests(DbContextOptions<ConfigurationContext> contextOptions)
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

                IEnumerable<User> users = Enumerable.Range(1, 20)
                    .Select(x =>
                    {

                        var user = new User
                        {
                            FirstName = $"Tal_{x}",
                            LastName = $"Almog_{x}",
                            Username = $"TalAlmog45_{x}",
                            Email = $"email_{x}"
                        };

                        using (var hmac = new HMACSHA512())
                        {
                            user.PasswordSalt = hmac.Key;
                            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("test-password"));
                        }

                        return user;
                    });


                context.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}
