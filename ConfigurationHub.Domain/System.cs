using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ConfigurationHub.Domain
{
    public class System
    {
        public int Id { get; set;}
        public string MicroserviceName { get; set; }

        [JsonIgnore]
        public List<Config> Configs { get; set; }
    }
}