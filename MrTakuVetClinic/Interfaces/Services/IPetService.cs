using MrTakuVetClinic.DTOs.Pet;
using MrTakuVetClinic.Models;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Services
{
    public interface IPetService
    {
        Task<ApiResponse<PetDto>> GetAllPetsAsync();
        Task<ApiResponse<PetDto>> GetPetByIdAsync(int id);
        Task<ApiResponse<PetDto>> UpdatePetByIdAsync(int id, PetUpdateDto petUpdateDto);
        Task<ApiResponse<PetDto>> PostPetAsync(PetPostDto petPostDto);
        Task<ApiResponse<PetDto>> DeletePetAsync(int id);
    }
}
