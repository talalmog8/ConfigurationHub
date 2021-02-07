using System;
using System.ComponentModel.DataAnnotations;

namespace ConfigurationHub.Domain.ConfigModels
{
    public class UpdateConfigDto
    {
        [Range(1, Int32.MaxValue)] public int Id { get; set; }
        [Required] public UpdatedConfigContentDto ConfigContent { get; set; }
    }
}