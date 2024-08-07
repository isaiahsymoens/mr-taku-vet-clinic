using AutoMapper;
using MrTakuVetClinic.DTOs.PetType;
using MrTakuVetClinic.Entities;

namespace MrTakuVetClinic.Mappings
{
    public class PetTypeProfile : Profile
    {
        public PetTypeProfile()
        {
            CreateMap<PetType, PetTypeDto>();
            CreateMap<PetTypePostDto, PetType>();
            CreateMap<PetTypeDto, PetType>();
        }
    }
}
