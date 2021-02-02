﻿using System.ComponentModel.DataAnnotations;

namespace ConfigurationHub.Domain.ConfigModels.SystemModels
{
    public class ExistingSystemDto
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
    }
}