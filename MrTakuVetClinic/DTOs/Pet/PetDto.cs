﻿using MrTakuVetClinic.DTOs.User;
using System;

namespace MrTakuVetClinic.DTOs.Pet
{
    public class PetDto
    {
        public string PetName { get; set; }
        public int PetTypeId { get; set; }
        public string Breed { get; set; }
        public DateTime BirthDate { get; set; }
        public UserDto User { get; set; }
    }
}
