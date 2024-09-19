using MrTakuVetClinic.DTOs.Pet;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Repositories
{
    public interface IPetRepository : IRepository<Pet>
    {
        Task<IEnumerable<Pet>> GetAllPetsAsync();
        Task<PaginatedResponse<Pet>> GetPaginatedPetsAsync(PaginationParameters paginationParams, PetSortDto petSortDto);
        Task<IEnumerable<Pet>> GetAllUserPetsAsync(string username);
        Task<PaginatedResponse<Pet>> GetAllPaginatedUserPetsAsync(string username, PaginationParameters paginationParams, PetSortDto petSortDto);
        Task<Pet> GetPetByIdAsync(int id);
    }
}
