using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MrTakuVetClinic.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int UserType { get; set; }
        public bool Active { get; set; }
        public ICollection<Pet> Pets { get; set; }
    }
}
