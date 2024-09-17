using MrTakuVetClinic.DTOs.Pet;
using MrTakuVetClinic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Services
{
    public interface IPetService
    {
        Task<ApiResponse<IEnumerable<PetDto>>> GetAllPetsAsync();
        Task<ApiResponse<PaginatedResponse<PetDto>>> GetAllPaginatedPetsAsync(PaginationParameters paginationParams);
        Task<ApiResponse<PetDto>> GetPetByIdAsync(int id);
        Task<ApiResponse<IEnumerable<PetDto>>> GetUserPetsByUsernameAsync(string username);
        Task<ApiResponse<PaginatedResponse<PetDto>>> GetPaginatedUserPetsByUsernameAsync(string username, PaginationParameters paginationParams);
        Task<ApiResponse<PetDto>> UpdatePetByIdAsync(int id, PetUpdateDto petUpdateDto);
        Task<ApiResponse<PetDto>> PostPetAsync(PetPostDto petPostDto);
        Task<ApiResponse<PetDto>> DeletePetAsync(int id);
    }
}
