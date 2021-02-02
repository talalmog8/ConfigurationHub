using System.ComponentModel.DataAnnotations;
using ConfigurationHub.Domain.ConfigModels.SystemModels;

namespace ConfigurationHub.Domain.ConfigModels.MicroserviceModels
{
    public class NewMicroServiceDto
    {
        [Required] public string Name { get; set; }

        [Required] public ExistingSystemDto System { get; set; }
    }
}
