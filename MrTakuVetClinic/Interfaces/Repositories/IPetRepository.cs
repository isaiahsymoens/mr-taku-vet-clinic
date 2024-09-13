using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Repositories
{
    public interface IPetRepository : IRepository<Pet>
    {
        Task<IEnumerable<Pet>> GetAllPetsAsync();
        Task<PaginatedResponse<Pet>> GetPaginatedPetsAsync(PaginationParameters paginationParams);
        Task<IEnumerable<Pet>> GetAllUserPetsAsync(string username);
        Task<Pet> GetPetByIdAsync(int id);
    }
}
