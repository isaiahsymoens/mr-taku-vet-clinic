using System;
using System.ComponentModel.DataAnnotations;

namespace MrTakuVetClinic.Entities
{
    public class Pet
    {
        [Key]
        public int PetId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string PetName { get; set; }
        [Required]
        public string PetType { get; set; }
        public string Breed { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
