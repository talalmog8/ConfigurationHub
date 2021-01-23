using System;
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
        public User Author { get; set; }
        public ConfigContent ConfigContent { get; set; }
        public Microservice Microservice { get; set; }
    }
}