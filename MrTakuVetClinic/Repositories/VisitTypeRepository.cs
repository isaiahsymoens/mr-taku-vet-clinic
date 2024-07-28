using MrTakuVetClinic.Data;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces;

namespace MrTakuVetClinic.Repositories
{
    public class VisitTypeRepository : Repository<VisitType>, IVisitTypeRepository
    {
        public VisitTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
