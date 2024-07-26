using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Repositories
{
    public class PetRepository : Repository<Pet>, IPetRepository
    {
        public PetRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Pet> GetPetByIdAsync(int id)
        {
            return await _context.Pets
                //.Include(p => p.PetId)
                //.Include(p => p.Breed)
                .FirstOrDefaultAsync(p => p.PetId == id);
        }
    }
}
