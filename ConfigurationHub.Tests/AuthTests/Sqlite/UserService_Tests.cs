using System;
using System.Linq;
using System.Text;
using ConfigurationHub.Data;
using ConfigurationHub.Data.Repositories;
using ConfigurationHub.Domain.Auth;
using ConfigurationHub.Tests.ConfigTests.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ConfigurationHub.Tests.AuthTests.Sqlite
{
    public class UserService_Tests : AuthTests
    {
        public UserService_Tests() : base(new DbContextOptionsBuilder<UserContext>()
        .UseSqlite($"Filename={nameof(UserService_Tests)}.db")
            .Options)
        {

        }

        [Fact]
        public void CanCreateUser()
        {
            using (var context = new UserContext(ContextOptions))
            {
                var userService = new UserService(context);

                var user = new User
                {
                    FirstName = "firstName",
                    LastName = "lastName",
                    Username = "username"
                };

                var actual = userService.Create(user, "test-password-1");

                using (var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                    user.PasswordSalt = hmac.Key;
                    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("test-password-1"));
                }

                Assert.Equal(user.PasswordHash, actual.PasswordHash);
                Assert.Equal(user.PasswordSalt, actual.PasswordSalt);
                Assert.Equal(user.Username, actual.Username);
                Assert.Equal(user.FirstName, actual.FirstName);
                Assert.Equal(user.LastName, actual.LastName);
            }
        }

        [Fact]
        public void UserNameUnique()
        {
            using (var context = new UserContext(ContextOptions))
            {
                var userService = new UserService(context);
                
                var user = new User
                {
                    FirstName = "Tal_1",
                    LastName = "Almog_1",
                    Username = "TalAlmog45_1"
                };

                Assert.Throws<ArgumentException>(() => userService.Create(user, "test-password-1000"));
            }
        }


        [Fact]
        public void CanDelete()
        {
            using (var context = new UserContext(ContextOptions))
            {
                var userService = new UserService(context);

                var user = new User
                {
                    FirstName = "firstName",
                    LastName = "lastName",
                    Username = "username"
                };

                var actual = userService.Create(user, "test-password-1");

                using (var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                    user.PasswordSalt = hmac.Key;
                    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("test-password-1"));
                }

                userService.Delete(actual.Id);
                Assert.Null(userService.GetById(actual.Id));
            }
        }

        [Fact]
        public void CanUpdate()
        {
            using (var context = new UserContext(ContextOptions))
            {
                var userService = new UserService(context);

                var user = context.Users.First();

                user.PasswordHash = null;
                user.PasswordSalt = null;

                userService.Update(user, "new-password");
                
                using (var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                    user.PasswordSalt = hmac.Key;
                    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("new-password"));
                }

                Assert.Equal(userService.GetById(user.Id).PasswordSalt, user.PasswordSalt);
                Assert.Equal(userService.GetById(user.Id).PasswordHash, user.PasswordHash);
            }
        }
    }
}
