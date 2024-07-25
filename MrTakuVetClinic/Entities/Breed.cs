using System.ComponentModel.DataAnnotations;

namespace MrTakuVetClinic.Entities
{
    public class Breed
    {
        [Key]
        public int BreedId { get; set; }
        [Required]
        [StringLength(50)]
        public string BreedName { get; set; }
    }
}
