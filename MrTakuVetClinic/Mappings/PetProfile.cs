using AutoMapper;
using MrTakuVetClinic.DTOs.Pet;
using MrTakuVetClinic.Entities;

namespace MrTakuVetClinic.Mappings
{
    public class PetProfile : Profile
    {
        public PetProfile()
        {
            CreateMap<Pet, PetDto>();
            CreateMap<PetPostDto, Pet>();
            CreateMap<PetDto, Pet>();
        }
    }
}
