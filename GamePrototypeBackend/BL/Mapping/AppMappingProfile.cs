using AutoMapper;
using GamePrototypeBackend.Data.Models;
using GamePrototypeBackend.Data.Repository.Models;

namespace GamePrototypeBackend.BL.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<User, UserModelDTO>();
        }
    }
}
