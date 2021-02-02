using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConfigurationHub.Domain.ConfigModels.MicroserviceModels
{
    public class Microservice
    {
        [Required] public int Id { get; set;}
        [Required] public string Name { get; set; }

        [Required] public SystemModels.System System { get; set; }
        public int SystemId { get; set; }
        public List<Config> Configs { get; set; }
    }
}