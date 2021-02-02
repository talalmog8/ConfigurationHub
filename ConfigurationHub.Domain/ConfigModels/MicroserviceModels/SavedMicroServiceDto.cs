using ConfigurationHub.Domain.ConfigModels.SystemModels;

namespace ConfigurationHub.Domain.ConfigModels.MicroserviceModels
{
    public class SavedMicroServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SavedSystemDto System { get; set; }
    }
}