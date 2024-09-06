using AutoMapper;
using MrTakuVetClinic.DTOs.Pet;
using MrTakuVetClinic.Entities;

namespace MrTakuVetClinic.Mappings
{
    public class PetProfile : Profile
    {
        public PetProfile()
        {
            CreateMap<Pet, PetDto>()
                //.ForMember(dest => dest.PetType, opt => opt.MapFrom(src => src.PetType.TypeName))
                .ForMember(dest => dest.NumberOfVisits, opt => opt.MapFrom(src => src.Visits.Count));
            CreateMap<PetPostDto, Pet>();
            CreateMap<PetDto, Pet>();
        }
    }
}
