using AutoMapper;
using MrTakuVetClinic.DTOs.User;
using MrTakuVetClinic.Entities;

namespace MrTakuVetClinic.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
