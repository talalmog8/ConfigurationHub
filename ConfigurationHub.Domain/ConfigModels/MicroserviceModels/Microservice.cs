using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationHub.Domain.ConfigModels.MicroserviceModels
{
    [Index(nameof(Name), IsUnique = true)]
    public class Microservice
    {
        [Required] public int Id { get; set;}
        [Required] public string Name { get; set; }

        [Required] public SystemModels.System System { get; set; }
        public int SystemId { get; set; }
        public List<Config> Configs { get; set; }
    }
}