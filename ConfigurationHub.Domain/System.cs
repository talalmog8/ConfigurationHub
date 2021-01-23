using System.Collections.Generic;

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
        public List<Microservice> Microservices { get; set; }
    }
}
