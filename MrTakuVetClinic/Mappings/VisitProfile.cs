using AutoMapper;
using MrTakuVetClinic.DTOs.Visit;
using MrTakuVetClinic.Entities;

namespace MrTakuVetClinic.Mappings
{
    public class VisitProfile : Profile
    {
        public VisitProfile()
        {
            CreateMap<Visit, VisitDto>();
            CreateMap<VisitPostDto, Visit>();
            CreateMap<VisitDto, Visit>();
        }
    }
}
