using System;
using System.Linq;
using System.Text.Json;
using Configuration.Data;
using Configuration.Domain;
using ConfigurationHub.Controllers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Xunit;

namespace ConfigurationHub.Tests
{
    public class SqliteConfigsControllerTest : ConfigsControllerTests
    {
        public SqliteConfigsControllerTest() : base(new DbContextOptionsBuilder<ConfigurationContext>()
            .UseSqlite("Filename=Test.db")
            .Options)
        {

        }

        [Fact]
        public void CanGetItems()
        {
            using (var context = new ConfigurationContext(ContextOptions))
            {
                var controller = new ConfigsController(context);

                var items = controller.GetConfigs().Result.Value.ToList();
                
                Assert.Equal("Tal", items[0].Author.FirstName);
                Assert.Equal("ConfigurationHub", items[0].System.MicroserviceName);
                Assert.Equal(200, JObject.Parse(items[0].ConfigContent.Content).GetValue("Port"));
                Assert.Single(items);
            }
        }

        [Fact]
        public void CanAddItem()
        {
            var author = new ConfigAuthor()
            {
                FirstName = "Yonatan",
                LastName = "Cohen"
            };

            var content = new ConfigContent()
            {
                Content = JsonSerializer.Serialize(new { Port = 2000 })
            };

            var system = new Configuration.Domain.System()
            {
                MicroserviceName = "SystemName"
            };

            var config = new Config()
            {
                ConfigContent = content,
                Author = author,
                System = system

            };

            using (var context = new ConfigurationContext(ContextOptions))
            {
                var controller = new ConfigsController(context);

                controller.PostConfig(config).Wait();
            }

            using (var context = new ConfigurationContext(ContextOptions))
            {
                var controller = new ConfigsController(context);

                var item = controller.GetConfigs().Result.Value.ToList()[1];

                Assert.Equal("Yonatan", item.Author.FirstName);
                Assert.Equal("SystemName", item.System.MicroserviceName);
                Assert.Equal(2000, JObject.Parse(item.ConfigContent.Content).GetValue("Port"));
            }
        }

        [Fact]
        public void CanUpdateItem()
        {
            Config config;

            using (var context = new ConfigurationContext(ContextOptions))
            {
                var controller = new ConfigsController(context);

                config = controller.GetConfigs().Result.Value.ElementAt(0);
            }

            using (var context = new ConfigurationContext(ContextOptions))
            {
                var controller = new ConfigsController(context);

                config.LastModified = new DateTime(2000, 1, 1, 1, 1, 1);

                controller.PutConfig(config.Id, config).Wait();
            }


            using (var context = new ConfigurationContext(ContextOptions))
            {
                var controller = new ConfigsController(context);

                var actual = controller.GetConfig(config.Id).Result.Value;

                Assert.Equal(new DateTime(2000, 1, 1, 1, 1, 1), actual.LastModified);
            }
        }

        [Fact]
        public void CanDeleteItem()
        {
            using (var context = new ConfigurationContext(ContextOptions))
            {
                var controller = new ConfigsController(context);

                var config = controller.GetConfigs().Result.Value.ElementAt(0);
                controller.DeleteConfig(config.Id).Wait();
                Assert.Throws<AggregateException>(() => controller.GetConfig(config.Id).Result);
            }
        }
    }
}   
