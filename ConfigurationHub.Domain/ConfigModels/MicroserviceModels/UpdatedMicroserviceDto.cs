using System.ComponentModel.DataAnnotations;

namespace ConfigurationHub.Domain.ConfigModels.MicroserviceModels
{
    public class UpdatedMicroserviceDto : NewMicroServiceDto
    {
        [Range(1, int.MaxValue)] public int Id { get; set; }
    }
}
