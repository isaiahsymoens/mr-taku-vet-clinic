using MrTakuVetClinic.Entities;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Repositories
{
    public interface IUserTypeRepository : IRepository<UserType>
    {
        Task<bool> IsTypeNameExits(string typeName);
    }
}
