using System.Collections.Generic;

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
        
        public List<Config> Configs { get; set; }
    }
}