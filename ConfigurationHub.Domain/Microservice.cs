using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ConfigurationHub.Domain
{
    public class Microservice
    {
        public int Id { get; set;}
        public string Name { get; set; }

        [JsonIgnore]
        public List<Config> Configs { get; set; }
    }
}