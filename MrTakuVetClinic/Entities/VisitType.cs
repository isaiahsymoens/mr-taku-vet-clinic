using System.ComponentModel.DataAnnotations;

namespace MrTakuVetClinic.Entities
{
    public class VisitType
    {
        [Key]
        public int VisitTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string TypeName { get; set; }
    }
}
