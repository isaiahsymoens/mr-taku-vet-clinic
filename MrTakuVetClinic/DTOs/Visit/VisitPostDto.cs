using System;

namespace MrTakuVetClinic.DTOs.Visit
{
    public class VisitPostDto
    {
        public int VisitTypeId { get; set; }
        public int PetId { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
    }
}
