using System.ComponentModel.DataAnnotations;

namespace ConfigurationHub.Domain.ConfigModels.Content
{
    public class ExistingConfigContent
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
    }
}
