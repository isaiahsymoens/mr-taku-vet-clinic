using System.ComponentModel.DataAnnotations;

namespace MrTakuVetClinic.Entities
{
    public class UserType
    {
        [Key]
        public int UserTypeId { get; set; }
        [Required]
        [StringLength(100)]
        public string TypeName { get; set; }
    }
}
