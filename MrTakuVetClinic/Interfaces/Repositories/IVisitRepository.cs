using MrTakuVetClinic.DTOs.Visit;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Repositories
{
    public interface IVisitRepository : IRepository<Visit>
    {
        Task<IEnumerable<Visit>> GetAllVisitsAsync();
        Task<PaginatedResponse<Visit>> GetAllPaginatedVisitsAsync(PaginationParameters paginationParams, VisitSortDto visitSortDto);
        Task<Visit> GetVisitByIdAsync(int id);
        Task<PaginatedResponse<Visit>> GetPetVisitsByIdAsync(int id, PaginationParameters paginationParams, VisitSortDto visitSortDto);
        Task<PaginatedResponse<Visit>> SearchVisitsAsync(VisitSearchDto visitSearchDto, PaginationParameters paginationParams, VisitSortDto visitSortDto);
    }
}
