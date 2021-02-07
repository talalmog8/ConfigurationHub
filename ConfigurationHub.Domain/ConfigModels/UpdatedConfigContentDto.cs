using System;
using System.ComponentModel.DataAnnotations;
using ConfigurationHub.Domain.ConfigModels.Content;

namespace ConfigurationHub.Domain.ConfigModels
{
    public class UpdatedConfigContentDto : NewConfigContentDto
    {
        [Range(1, Int32.MaxValue)] public int Id { get; set; }
    }
}