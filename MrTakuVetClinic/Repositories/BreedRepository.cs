using MrTakuVetClinic.Data;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces;

namespace MrTakuVetClinic.Repositories
{
    public class BreedRepository : Repository<Breed>, IBreedRepository
    {
        public BreedRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
