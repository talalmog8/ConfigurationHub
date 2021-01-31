using System;
using System.Text.Json.Serialization;
using ConfigurationHub.Domain.Auth;

namespace ConfigurationHub.Domain
{
    public class Config
    {
        public Config()
        {
            LastModified = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public DateTime LastModified { get; set; }
        [JsonIgnore] public User Author { get; set; }
        public ConfigContent ConfigContent { get; set; }
        public Microservice Microservice { get; set; }
    }
}