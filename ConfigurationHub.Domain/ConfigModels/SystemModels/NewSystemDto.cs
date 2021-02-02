using System.ComponentModel.DataAnnotations;

namespace ConfigurationHub.Domain.ConfigModels.SystemModels
{
    public class NewSystemDto
    {
        [Required] public string Name { get; set; }
    }
}
