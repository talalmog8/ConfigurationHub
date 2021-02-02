using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ConfigurationHub.Domain.ConfigModels;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationHub.Domain.Auth
{
    [Index(nameof(Username), nameof(Email), IsUnique = true)]
    public class User
    {
        public User()
        {
            Configs = new List<Config>();
        }

        [Key] public int Id { get; set; } 
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string Username { get; set; }
        [EmailAddress] public string Email { get; set; }

        [JsonIgnore] public List<Config> Configs { get; set; }

        [JsonIgnore] public byte[] PasswordHash { get; set; }
        [JsonIgnore] public byte[] PasswordSalt { get; set; }
    }
}
