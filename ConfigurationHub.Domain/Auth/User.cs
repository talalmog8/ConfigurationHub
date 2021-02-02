using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ConfigurationHub.Domain.ConfigModels;

namespace ConfigurationHub.Domain.Auth
{
    public class User
    {
        public User()
        {
            Configs = new List<Config>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        [JsonIgnore] public List<Config> Configs { get; set; }

        [JsonIgnore] public byte[] PasswordHash { get; set; }
        [JsonIgnore] public byte[] PasswordSalt { get; set; }
    }
}
