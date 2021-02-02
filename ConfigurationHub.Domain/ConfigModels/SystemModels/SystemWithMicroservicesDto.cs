using System.Collections.Generic;
using ConfigurationHub.Domain.ConfigModels.MicroserviceModels;

namespace ConfigurationHub.Domain.ConfigModels.SystemModels
{
    public class SystemWithMicroservicesDto : SavedSystemDto
    {
        public List<SavedMicroServiceDto> Microservices { get; set; } = new List<SavedMicroServiceDto>();
    }
}