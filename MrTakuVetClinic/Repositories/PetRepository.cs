using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Repositories
{
    public class PetRepository : Repository<Pet>, IPetRepository
    {
        public PetRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Pet>> GetAllPetsAsync()
        {
            return await _context.Pets
                .Include(p => p.PetType)
                .Include(p => p.User)
                .ThenInclude(p => p.UserType)
                .ToListAsync();
        }

        public async Task<Pet> GetPetByIdAsync(int id)
        {
            return await _context.Pets
                .Include(p => p.PetType)
                .Include(p => p.User)
                .ThenInclude(p => p.UserType)
                .FirstOrDefaultAsync(p => p.PetId == id);
        }
    }
}
