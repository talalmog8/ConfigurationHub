using System.ComponentModel.DataAnnotations;
using ConfigurationHub.Domain.ConfigModels.SystemModels;

namespace ConfigurationHub.Domain.ConfigModels.MicroserviceModels
{
    public class ExistingMicroServiceDto
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        public ExistingSystemDto System { get; set; }
    }
}