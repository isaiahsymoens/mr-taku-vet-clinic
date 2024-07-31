﻿using MrTakuVetClinic.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces
{
    public interface IPetRepository : IRepository<Pet>
    {
        Task<IEnumerable<Pet>> GetAllPetsAsync();
        Task<Pet> GetPetByIdAsync(int id);
    }
}
