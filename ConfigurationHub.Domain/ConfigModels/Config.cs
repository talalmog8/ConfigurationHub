using System;
using System.ComponentModel.DataAnnotations;
using ConfigurationHub.Domain.Auth;
using ConfigurationHub.Domain.ConfigModels.Content;
using ConfigurationHub.Domain.ConfigModels.MicroserviceModels;

namespace ConfigurationHub.Domain.ConfigModels
{
    public class Config
    {
        public Config()
        {
            LastModified = DateTime.UtcNow;
        }

        [Required] public int Id { get; set; }
        public DateTime LastModified { get; set; }
        [Required] public User Author { get; set; }
        [Required] public ConfigContent ConfigContent { get; set; }
        [Required] public Microservice Microservice { get; set; }
    }
}