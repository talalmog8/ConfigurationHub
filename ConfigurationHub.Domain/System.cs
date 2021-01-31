using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ConfigurationHub.Domain
{
    public class System
    {
        public System()
        {
            Microservices = new List<Microservice>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore] public List<Microservice> Microservices { get; set; }
    }
}
