using System.ComponentModel.DataAnnotations;

namespace MrTakuVetClinic.Entities
{
    public class PetType
    {
        [Key]
        public int PetTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string TypeName { get; set; }
    }
}
