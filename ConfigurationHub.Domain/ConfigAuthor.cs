using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Configuration.Domain
{
    public class ConfigAuthor
    {
        public ConfigAuthor()
        {
            Configs = new List<Config>();
        }
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        [JsonIgnore]
        public List<Config> Configs { get; set; }
    }
}