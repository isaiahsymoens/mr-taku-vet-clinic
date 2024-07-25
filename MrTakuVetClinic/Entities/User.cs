using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MrTakuVetClinic.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        [Required]
        public int UserTypeId { get; set; }
        public bool Active { get; set; }
        public UserType UserType { get; set; }
        public ICollection<Pet> Pets { get; set; }
    }
}
