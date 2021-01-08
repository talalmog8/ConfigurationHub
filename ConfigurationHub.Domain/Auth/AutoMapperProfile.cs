using AutoMapper;

namespace ConfigurationHub.Domain.Auth
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, AuthenticateDto>();
            CreateMap<RegisterDto, User>();
            CreateMap<UpdateDto, User>();
        }
    }
}
