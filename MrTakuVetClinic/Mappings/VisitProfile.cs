using AutoMapper;
using MrTakuVetClinic.DTOs.Visit;
using MrTakuVetClinic.Entities;

namespace MrTakuVetClinic.Mappings
{
    public class VisitProfile : Profile
    {
        public VisitProfile()
        {
            CreateMap<Visit, VisitDto>()
                .ForMember(dest => dest.VisitType, opt => opt.MapFrom(src => src.VisitType.TypeName));
            CreateMap<VisitPostDto, Visit>();
            CreateMap<VisitDto, Visit>();
        }
    }
}
