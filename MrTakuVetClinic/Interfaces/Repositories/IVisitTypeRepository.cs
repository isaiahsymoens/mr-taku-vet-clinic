using MrTakuVetClinic.Entities;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Repositories
{
    public interface IVisitTypeRepository : IRepository<VisitType>
    {
        Task<bool> IsTypeNameExits(string typeName);
    }
}
