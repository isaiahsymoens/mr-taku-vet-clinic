using MrTakuVetClinic.DTOs.Pet;
using System;

namespace MrTakuVetClinic.DTOs.Visit
{
    public class VisitDto
    {
        //public int VisitType { get; set; }
        public DateTime Date { get; set; }
        public int PetId { get; set; }
        public string Notes { get; set; }
        public PetDto Pet { get; set; }
    }
}
