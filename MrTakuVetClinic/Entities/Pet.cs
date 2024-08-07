using System;
using System.Collections.Generic;

namespace MrTakuVetClinic.Entities
{
    public class Pet
    {
        public int PetId { get; set; }
        public int UserId { get; set; }
        public string PetName { get; set; }
        public int PetTypeId { get; set; }
        public string Breed { get; set; }
        public DateTime BirthDate { get; set; }
        public User User { get; set; }
        public PetType PetType { get; set; }
        public ICollection<Visit> Visits { get; set; }
    }
}
