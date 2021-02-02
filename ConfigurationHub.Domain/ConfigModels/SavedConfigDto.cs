using System;
using ConfigurationHub.Domain.ConfigModels.Content;
using ConfigurationHub.Domain.ConfigModels.MicroserviceModels;

namespace ConfigurationHub.Domain.ConfigModels
{
    public class SavedConfigDto
    {
        public int Id { get; set; }
        public DateTime LastModified { get; set; }

        public SavedConfigContentDto ConfigContent { get; set; }
        public SavedMicroServiceDto Microservice { get; set; }
    }
}