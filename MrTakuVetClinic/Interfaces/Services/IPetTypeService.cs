using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Models;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Services
{
    public interface IPetTypeService
    {
        Task<ApiResponse<PetType>> GetAllPetTypesAsync();
        Task<ApiResponse<PetType>> GetPetTypeByIdAsync(int id);
        Task<ApiResponse<PetType>> PostPetTypeAsync(PetType petType);
    }
}
