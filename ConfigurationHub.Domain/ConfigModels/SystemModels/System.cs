using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ConfigurationHub.Domain.ConfigModels.MicroserviceModels;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationHub.Domain.ConfigModels.SystemModels
{
    [Index(nameof(Name), IsUnique = true)]
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
