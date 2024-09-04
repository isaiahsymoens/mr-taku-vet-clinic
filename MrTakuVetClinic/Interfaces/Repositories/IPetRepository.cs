﻿using MrTakuVetClinic.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Repositories
{
    public interface IPetRepository : IRepository<Pet>
    {
        Task<IEnumerable<Pet>> GetAllPetsAsync();
        Task<IEnumerable<Pet>> GetAllUserPetsAsync(string username);
        Task<Pet> GetPetByIdAsync(int id);
    }
}
