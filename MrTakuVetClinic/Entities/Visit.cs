using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MrTakuVetClinic.Entities
{
    public class Visit
    {
        [Key]
        public int VisitId { get; set; }
        [Required]
        public int VisitTypeId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int PetId { get; set; }
        [Required]
        public string Notes { get; set; }
        public Pet Pet { get; set; }
        public VisitType VisitType { get; set; }
    }
}
