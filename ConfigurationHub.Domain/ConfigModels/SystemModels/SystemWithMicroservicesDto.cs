using System.Collections.Generic;
using ConfigurationHub.Domain.ConfigModels.MicroserviceModels;

namespace ConfigurationHub.Domain.ConfigModels.SystemModels
{
    public class SystemWithMicroservicesDto : SavedSystemDto
    {
        public List<SavedMicroServiceShallowDto> Microservices { get; set; } = new List<SavedMicroServiceShallowDto>();
    }

    public class SavedMicroServiceShallowDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}