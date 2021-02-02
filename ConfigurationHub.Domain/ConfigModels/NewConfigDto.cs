using System.ComponentModel.DataAnnotations;
using ConfigurationHub.Domain.ConfigModels.Content;
using ConfigurationHub.Domain.ConfigModels.MicroserviceModels;

namespace ConfigurationHub.Domain.ConfigModels
{
    public class NewConfigDto
    {
        [Required] public NewConfigContentDto ConfigContent { get; set; }
        [Required] public ExistingMicroServiceDto Microservice { get; set; }
    }
}
