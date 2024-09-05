using System;

namespace MrTakuVetClinic.DTOs.Visit
{
    public class VisitUpdateDto
    {
        public int? VisitTypeId { get; set; }
        public DateTime Date { get; set; }
        public int? PetId { get; set; }
        public string Notes { get; set; }
    }
}
