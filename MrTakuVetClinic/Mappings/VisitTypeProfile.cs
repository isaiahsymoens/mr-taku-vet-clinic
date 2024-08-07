using AutoMapper;
using MrTakuVetClinic.DTOs.VisitType;
using MrTakuVetClinic.Entities;

namespace MrTakuVetClinic.Mappings
{
    public class VisitTypeProfile : Profile
    {
        public VisitTypeProfile()
        {
            CreateMap<VisitType, VisitTypeDto>();
            CreateMap<VisitTypePostDto, VisitType>();
            CreateMap<VisitTypeDto, VisitType>();
        }
    }
}
