using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
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
                .Include(p => p.Visits)
                .Include(p => p.PetType)
                .Include(p => p.User)
                .ThenInclude(p => p.UserType)
                .OrderBy(p => p.PetName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pet>> GetAllUserPetsAsync(string username)
        {
            return await _context.Pets
                .Where(p => p.User.Username == username)
                .Include(p => p.Visits)
                .Include(p => p.PetType)
                .Include(p => p.User)
                .ThenInclude(p => p.UserType)
                .OrderBy(p => p.PetName)
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
