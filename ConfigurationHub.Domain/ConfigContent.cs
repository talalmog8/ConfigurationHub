using System.Text.Json.Serialization;

namespace ConfigurationHub.Domain
{
    public class ConfigContent
    {
        public int Id { get; set; }
        public string Content { get; set; }
        
        [JsonIgnore]
        public Config Config { get; set; }
        public int ConfigId { get; set; }
    }
}