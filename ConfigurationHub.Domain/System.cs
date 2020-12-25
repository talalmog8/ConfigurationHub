using System.Collections.Generic;

namespace Configuration.Domain
{
    public class System
    {
        public int Id { get; set;}
        public string MicroserviceName { get; set; }
        public List<Config> Configs { get; set; }
    }
}