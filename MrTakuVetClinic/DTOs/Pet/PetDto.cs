using MrTakuVetClinic.DTOs.User;
using System;

namespace MrTakuVetClinic.DTOs.Pet
{
    public class PetDto
    {
        public int PetId { get; set; }
        public string PetName { get; set; }
        public string PetType { get; set; }
        public string Breed { get; set; }
        public DateTime BirthDate { get; set; }
        public int NumberOfVisits { get; set; }
        public UserDto User { get; set; }
    }
}
