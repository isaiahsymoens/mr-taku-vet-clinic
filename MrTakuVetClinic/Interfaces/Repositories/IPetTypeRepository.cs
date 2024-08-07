using MrTakuVetClinic.Entities;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Repositories
{
    public interface IPetTypeRepository : IRepository<PetType>
    {
        Task<bool> IsTypeNameExits(string typeName);
    }
}
