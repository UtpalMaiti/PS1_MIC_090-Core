using AutoMapper;

using Microsoft.AspNetCore.Identity;

using PS1_MIC_090_Core.Models;
using PS1_MIC_090_Core.Repository.Domain;

namespace PS1_MIC_090_Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<User, UserLoginInfo>().ReverseMap();
            CreateMap<Application, CreateApplicationViewModel>().ReverseMap();
        }
    }
}
