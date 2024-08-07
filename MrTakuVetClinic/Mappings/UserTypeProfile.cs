using AutoMapper;
using MrTakuVetClinic.DTOs.UserType;
using MrTakuVetClinic.Entities;

namespace MrTakuVetClinic.Mappings
{
    public class UserTypeProfile : Profile
    {
        public UserTypeProfile()
        {
            CreateMap<UserType, UserTypeDto>();
            CreateMap<UserTypePostDto, UserType>();
            CreateMap<UserTypeDto, UserType>();
        }
    }
}
