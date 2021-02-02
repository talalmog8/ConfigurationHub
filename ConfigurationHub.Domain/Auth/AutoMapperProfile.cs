﻿using AutoMapper;
using ConfigurationHub.Domain.ConfigModels;
using ConfigurationHub.Domain.ConfigModels.Content;
using ConfigurationHub.Domain.ConfigModels.MicroserviceModels;
using ConfigurationHub.Domain.ConfigModels.SystemModels;

namespace ConfigurationHub.Domain.Auth
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, AuthenticateDto>();
            CreateMap<RegisterDto, User>();
            CreateMap<UpdateDto, User>();
            CreateMap<NewConfigDto, Config>();
            CreateMap<ExistingMicroServiceDto, Microservice>();
            CreateMap<ExistingSystemDto, ConfigModels.SystemModels.System>();
            CreateMap<NewConfigContentDto, ConfigContent>();
            CreateMap<Config, SavedConfigDto>();
            CreateMap<Microservice, SavedMicroServiceDto>();
            CreateMap<ConfigModels.SystemModels.System, SavedSystemDto>();
            CreateMap<ConfigContent, SavedConfigContentDto>();
        }
    }
}
