using MrTakuVetClinic.Entities;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces
{
    public interface IVisitRepository : IRepository<Visit>
    {
        Task<Visit> GetBVisitByIdAsync(int id);
    }
}
