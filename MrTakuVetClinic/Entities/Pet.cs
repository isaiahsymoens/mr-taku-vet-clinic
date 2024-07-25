using System;
using System.Collections.Generic;
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
        [StringLength(100)]
        public string PetName { get; set; }
        [Required]
        public int PetTypeId { get; set; }
        public int BreedId { get; set; }
        public DateTime BirthDate { get; set; }
        public User User { get; set; }
        public PetType PetType { get; set; }
        public Breed Breed { get; set; }
        public ICollection<Visit> Visits { get; set; }
    }
}
