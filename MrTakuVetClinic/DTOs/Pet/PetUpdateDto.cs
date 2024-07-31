using System.ComponentModel.DataAnnotations;
using System;

namespace MrTakuVetClinic.DTOs.Pet
{
    public class PetUpdateDto
    {
        public string PetName { get; set; }
        public int? PetTypeId { get; set; }
        public string Breed { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
