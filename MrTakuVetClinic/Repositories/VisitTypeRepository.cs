using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces.Repositories;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Repositories
{
    public class VisitTypeRepository : Repository<VisitType>, IVisitTypeRepository
    {
        public VisitTypeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> IsTypeNameExits(string typeName)
        {
            return await _context.VisitsTypes.AnyAsync(v => v.TypeName.ToLower().Trim() == typeName.ToLower().Trim());
        }
    }
}
