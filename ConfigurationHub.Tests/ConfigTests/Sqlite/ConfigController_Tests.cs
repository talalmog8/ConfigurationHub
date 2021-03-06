﻿using System;
using System.Linq;
using System.Text.Json;
using Configuration.Data;
using ConfigurationHub.Controllers;
using ConfigurationHub.Data.Repositories;
using ConfigurationHub.Domain;
using ConfigurationHub.Domain.Auth;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Xunit;

namespace ConfigurationHub.Tests.ConfigTests.Sqlite
{
    public class ConfigController_Tests : ConfigControllersTests
    {
        public ConfigController_Tests() : base(new DbContextOptionsBuilder<ConfigurationContext>()
            .UseSqlite($"Filename={nameof(ConfigController_Tests)}.db")
            .Options)
        {

        }

        [Fact]
        public async void CanGetItems()
        {
            await using (var context = new ConfigurationContext(ContextOptions))
            {
                var controller = new ConfigsController(context);

                var items = (await controller.GetConfigs()).Value.ToList();

                Assert.Equal("Tal", items[0].Author.FirstName);
                Assert.Equal("ConfigurationHub", items[0].Microservice.Name);
                Assert.Equal(200, JObject.Parse(items[0].ConfigContent.Content).GetValue("Port"));
                Assert.Single(items);
            }
        }

        [Fact]
        public async void CanAddItem()
        {
            var author = new User
            {
                Email = ".com",
                FirstName = "Tal",
                LastName = "Almog",
                Username = "abc"

            };

            var content = new ConfigContent
            {
                Content = JsonSerializer.Serialize(new { Port = 2000 })
            };

            var system = new Microservice
            {
                Name = "SystemName"
            };

            var config = new Config
            {
                ConfigContent = content,
                Author = author,
                Microservice = system

            };

            await using (var context = new ConfigurationContext(ContextOptions))
            {
                author = new UserService(context).Register(author, "pass");

                var controller = new ConfigsController(context);

                await controller.PostConfig(config);
            }

            await using (var context = new ConfigurationContext(ContextOptions))
            {
                var controller = new ConfigsController(context);

                var item = (await controller.GetConfigs()).Value.ToList()[1];

                Assert.Equal("Tal", item.Author.FirstName);
                Assert.Equal("SystemName", item.Microservice.Name);
                Assert.Equal(2000, JObject.Parse(item.ConfigContent.Content).GetValue("Port"));
            }
        }

        [Fact]
        public async void CanUpdateItem()
        {
            Config config;

            await using (var context = new ConfigurationContext(ContextOptions))
            {
                var controller = new ConfigsController(context);

                config = (await controller.GetConfigs()).Value.ElementAt(0);
            }

            await using (var context = new ConfigurationContext(ContextOptions))
            {
                var controller = new ConfigsController(context);

                config.LastModified = new DateTime(2000, 1, 1, 1, 1, 1);

                await controller.PutConfig(config.Id, config);
            }


            await using (var context = new ConfigurationContext(ContextOptions))
            {
                var controller = new ConfigsController(context);

                var actual = (await controller.GetConfig(config.Id)).Value;

                Assert.Equal(new DateTime(2000, 1, 1, 1, 1, 1), actual.LastModified);
            }
        }

        [Fact]
        public async void CanDeleteItem()
        {
            await using (var context = new ConfigurationContext(ContextOptions))
            {
                var controller = new ConfigsController(context);

                var config = (await controller.GetConfigs()).Value.ElementAt(0);
                await controller.DeleteConfig(config.Id);
                Assert.Throws<AggregateException>(() => (controller.GetConfig(config.Id)).Result.Value);
            }
        }
    }
}
