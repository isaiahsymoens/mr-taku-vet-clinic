using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Helpers;
using MrTakuVetClinic.Interfaces;
using MrTakuVetClinic.Models;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Services
{
    public class PetTypeService
    {
        private readonly IPetTypeRepository _petTypeRepository;
        public PetTypeService(IPetTypeRepository petTypeRepository)
        {
            _petTypeRepository = petTypeRepository;
        }

        public async Task<ApiResponse<PetType>> GetAllPetTypesAsync()
        {
            return ApiResponseHelper
                .SuccessResponse<PetType>(200, await _petTypeRepository.GetAllAsync());
        }

        public async Task<ApiResponse<PetType>> GetPetTypeByIdAsync(int id)
        {
            var petType = await _petTypeRepository.GetByIdAsync(id);
            if (petType == null)
            {
                return ApiResponseHelper.FailResponse<PetType>(404, new { Message = "Pet type does not exist." });

            }
            return ApiResponseHelper
                .SuccessResponse<PetType>(200, petType);
        }

        public async Task<ApiResponse<PetType>> PostPetTypeAsync(PetType petType)
        {
            await _petTypeRepository.AddAsync(petType);
            return ApiResponseHelper
                .SuccessResponse<PetType>(204, null);
        }
    }
}
