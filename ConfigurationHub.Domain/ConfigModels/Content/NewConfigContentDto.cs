using System.ComponentModel.DataAnnotations;

namespace ConfigurationHub.Domain.ConfigModels.Content
{
    public class NewConfigContentDto
    {
        [Required]
        public string Content { get; set; }
    }
}