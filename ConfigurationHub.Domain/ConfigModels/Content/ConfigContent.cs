using System.ComponentModel.DataAnnotations;

namespace ConfigurationHub.Domain.ConfigModels.Content
{
    public class ConfigContent
    {
        [Key] public int Id { get; set; }

        [Required] public string Content { get; set; }
        
        public Config Config { get; set; }

        public int ConfigId { get; set; }
    }
}