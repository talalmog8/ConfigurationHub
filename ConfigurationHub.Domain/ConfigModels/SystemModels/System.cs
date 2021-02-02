using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ConfigurationHub.Domain.ConfigModels.MicroserviceModels;

namespace ConfigurationHub.Domain.ConfigModels.SystemModels
{
    public class System
    {
        public System()
        {
            Microservices = new List<Microservice>();
        }

        [Required] public int Id { get; set; }
        [Required] public string Name { get; set; }
        public List<Microservice> Microservices { get; set; }
    }
}
