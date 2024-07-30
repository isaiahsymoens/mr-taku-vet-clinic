using System;

namespace MrTakuVetClinic.DTOs.Visit
{
    public class VisitFilterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PetName { get; set; }
        public string PetType { get; set; }
        public string VisitType { get; set; }
        public DateTime? VisitDateFrom { get; set; }
        public DateTime? VisitDateTo { get; set; }
    }
}
