using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigurationHub.Data;
using ConfigurationHub.Domain.Auth;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationHub.Tests.AuthTests
{
    public class AuthTests
    {
        protected DbContextOptions<UserContext> ContextOptions { get; }

        protected AuthTests(DbContextOptions<UserContext> contextOptions)
        {
            ContextOptions = contextOptions;
            Seed();
        }

        private void Seed()
        {
            using (var context = new UserContext(ContextOptions))
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
                            Username = $"TalAlmog45_{x}"
                        };

                        using (var hmac = new System.Security.Cryptography.HMACSHA512())
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
