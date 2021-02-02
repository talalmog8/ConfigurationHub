using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using ConfigurationHub.Domain.Auth;
using ConfigurationHub.Domain.ConfigModels.Content;
using ConfigurationHub.Domain.ConfigModels.MicroserviceModels;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationHub.Domain.ConfigModels
{
    [Index(nameof(LastModified))]
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