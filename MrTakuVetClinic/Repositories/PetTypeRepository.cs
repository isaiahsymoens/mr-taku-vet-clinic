using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces.Repositories;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Repositories
{
    public class PetTypeRepository : Repository<PetType>, IPetTypeRepository
    {
        public PetTypeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> IsTypeNameExits(string typeName)
        {
            return await _context.PetTypes.AnyAsync(u => u.TypeName == typeName);
        }
    }
}
