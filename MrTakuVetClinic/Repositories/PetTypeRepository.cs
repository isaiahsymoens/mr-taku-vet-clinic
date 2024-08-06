using MrTakuVetClinic.Data;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces.Repositories;

namespace MrTakuVetClinic.Repositories
{
    public class PetTypeRepository : Repository<PetType>, IPetTypeRepository
    {
        public PetTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
