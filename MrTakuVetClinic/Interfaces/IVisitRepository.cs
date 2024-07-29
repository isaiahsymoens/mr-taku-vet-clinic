using MrTakuVetClinic.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces
{
    public interface IVisitRepository : IRepository<Visit>
    {
        Task<IEnumerable<Visit>> GetAllVisitsAsync();
        Task<Visit> GetBVisitByIdAsync(int id);
        Task<IEnumerable<Visit>> SearchVisitsAsync(string lastName, int? visitTypeId);
    }
}
