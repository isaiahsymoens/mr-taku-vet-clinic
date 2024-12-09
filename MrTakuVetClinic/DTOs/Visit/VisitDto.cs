using MrTakuVetClinic.DTOs.Pet;
using MrTakuVetClinic.DTOs.VisitType;
using System;

namespace MrTakuVetClinic.DTOs.Visit
{
    public class VisitDto
    {
        public int VisitId { get; set; }
        public VisitTypeDto VisitType { get; set; }
        public DateTime Date { get; set; }
        public int PetId { get; set; }
        public string Notes { get; set; }
        public PetDto Pet { get; set; }
    }
}
