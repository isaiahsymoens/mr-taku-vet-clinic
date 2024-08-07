using MrTakuVetClinic.DTOs.PetType;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Models;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Services
{
    public interface IPetTypeService
    {
        Task<ApiResponse<PetTypeDto>> GetAllPetTypesAsync();
        Task<ApiResponse<PetTypeDto>> GetPetTypeByIdAsync(int id);
        Task<ApiResponse<PetTypeDto>> PostPetTypeAsync(PetType petType);
    }
}
