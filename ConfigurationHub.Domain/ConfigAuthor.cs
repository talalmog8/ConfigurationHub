using System.Collections.Generic;
using System.Text.Json.Serialization;
using ConfigurationHub.Domain.Auth;

namespace ConfigurationHub.Domain
{
    public class ConfigAuthor
    {
        public ConfigAuthor()
        {
            Configs = new List<Config>();
        }

        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public List<Config> Configs { get; set; }
    }
}