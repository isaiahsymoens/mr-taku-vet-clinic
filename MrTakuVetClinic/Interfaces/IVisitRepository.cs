using MrTakuVetClinic.DTOs.Visit;
using MrTakuVetClinic.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces
{
    public interface IVisitRepository : IRepository<Visit>
    {
        Task<IEnumerable<Visit>> GetAllVisitsAsync();
        Task<Visit> GetBVisitByIdAsync(int id);
        Task<IEnumerable<Visit>> SearchVisitsAsync(VisitFilterDto visitFilterDto);
    }
}
