using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Repositories
{
    public class VisitRepository : Repository<Visit>, IVisitRepository
    {
        public VisitRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Visit> GetBVisitByIdAsync(int id)
        {
            return await _context.Visits
                .Include(v => v.VisitType)
                .FirstOrDefaultAsync(v => v.VisitId == id);
        }
    }
}
