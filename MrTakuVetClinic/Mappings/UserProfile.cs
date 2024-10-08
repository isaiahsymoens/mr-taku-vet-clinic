﻿using AutoMapper;
using MrTakuVetClinic.DTOs.User;
using MrTakuVetClinic.Entities;

namespace MrTakuVetClinic.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.UserType.TypeName));
            CreateMap<UserPostDto, User>();
            CreateMap<UserDto, User>();
        }
    }
}
