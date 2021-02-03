using System.Collections.Generic;
using ConfigurationHub.Domain.ConfigModels.SystemModels;

namespace ConfigurationHub.Domain.ConfigModels.MicroserviceModels
{
    public class SavedMicroServiceWithConfigsDto
    {
        public IEnumerable<int> ConfigIds { get; set; }

        public SavedMicroServiceDto MicroService { get; set; }
    }

    public class SavedMicroServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SavedSystemDto System { get; set; }
    }
}