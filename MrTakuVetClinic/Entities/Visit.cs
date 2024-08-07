using System;

namespace MrTakuVetClinic.Entities
{
    public class Visit
    {
        public int VisitId { get; set; }
        public int VisitTypeId { get; set; }
        public DateTime Date { get; set; }
        public int PetId { get; set; }
        public string Notes { get; set; }
        public Pet Pet { get; set; }
        public VisitType VisitType { get; set; }
    }
}
