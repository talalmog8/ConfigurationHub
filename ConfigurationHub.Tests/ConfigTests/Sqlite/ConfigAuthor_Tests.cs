using System;
using System.Linq;
using Configuration.Data;
using ConfigurationHub.Controllers;
using ConfigurationHub.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ConfigurationHub.Tests.ConfigTests.Sqlite
{
    public class ConfigAuthor_Tests : ConfigControllersTests
    {
        public ConfigAuthor_Tests() : base(
            new DbContextOptionsBuilder<ConfigurationContext>()
                .UseSqlite($"Filename={nameof(ConfigAuthor_Tests)}.db")
                .Options)
        {
        }


        [Fact]
        public async void CanGetAuthors()
        {
            await using (var context = new ConfigurationContext(ContextOptions))
            {
                var controller = new ConfigAuthorsController(context);

                var items = (await controller.GetConfigAuthors(5)).Value.ToList();
                Assert.Equal("Tal", items.ElementAt(0).ConfigAuthor.FirstName);
                Assert.Equal("Almog", items.ElementAt(0).ConfigAuthor.LastName);
                Assert.Equal(1, items.ElementAt(0).ConfigCount);
            }
        }

        [Fact]
        public async void CanAddAuthors()
        {
            await using (var context = new ConfigurationContext(ContextOptions))
            {
                var controller = new ConfigAuthorsController(context);
                var author = new ConfigAuthor()
                {
                    FirstName = "Client",
                    LastName = "Eastwood"
                };

                await controller.PostConfigAuthor(author);
                var actual = (await controller.GetConfigAuthor(author.Id)).Value;
                Assert.Equal("Client", actual.FirstName);
                Assert.Equal("Eastwood", actual.LastName);
            }
        }

        [Fact]
        public async void CanUpdateAuthor()
        {
            await using (var context = new ConfigurationContext(ContextOptions))
            {
                var controller = new ConfigAuthorsController(context);

                var configAuthor = (await controller.GetConfigAuthors(1)).Value.ElementAt(0);

                var expectedName = configAuthor.ConfigAuthor.FirstName = Guid.NewGuid().ToString();

                await controller.PutConfigAuthor(configAuthor.ConfigAuthor.Id, configAuthor.ConfigAuthor);

                var actualName = (await controller.GetConfigAuthor(configAuthor.ConfigAuthor.Id)).Value.FirstName;

                Assert.Equal(expectedName, actualName);
            }
        }

        [Fact]
        public async void CanDeleteAuthor()
        {
            ConfigAuthor actual;

            await using (var context = new ConfigurationContext(ContextOptions))
            {
                var controller = new ConfigAuthorsController(context);
                var author = new ConfigAuthor()
                {
                    FirstName = "Client1",
                    LastName = "Eastwood1"
                };

                await controller.PostConfigAuthor(author);
                actual = (await controller.GetConfigAuthor(author.Id)).Value;
                Assert.Equal("Client1", actual.FirstName);
                Assert.Equal("Eastwood1", actual.LastName);
            }

            await using (var context = new ConfigurationContext(ContextOptions))
            {
                var controller = new ConfigAuthorsController(context);
                await controller.DeleteConfigAuthor(actual.Id);
                Assert.IsType<NotFoundResult>((await controller.GetConfigAuthor(actual.Id)).Result);
            }
        }
    }
}