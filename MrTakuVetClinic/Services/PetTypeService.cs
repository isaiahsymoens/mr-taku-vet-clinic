using AutoMapper;
using FluentValidation;
using MrTakuVetClinic.DTOs.PetType;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Helpers;
using MrTakuVetClinic.Interfaces.Repositories;
using MrTakuVetClinic.Interfaces.Services;
using MrTakuVetClinic.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Services
{
    public class PetTypeService : IPetTypeService
    {
        private readonly IPetTypeRepository _petTypeRepository;
        private readonly IValidator<PetTypePostDto> _petTypeValidator;
        private readonly IMapper _mapper;

        public PetTypeService(
            IPetTypeRepository petTypeRepository, 
            IValidator<PetTypePostDto> petTypeValidator, 
            IMapper mapper)
        {
            _petTypeRepository = petTypeRepository;
            _petTypeValidator = petTypeValidator;
            _mapper = mapper;
        }

        public async Task<ApiResponse<PetTypeDto>> GetAllPetTypesAsync()
        {
            return ApiResponseHelper
                .SuccessResponse<PetTypeDto>(
                    200,
                    (await _petTypeRepository.GetAllAsync())
                    .Select(p => _mapper.Map<PetTypeDto>(p))
                );
        }

        public async Task<ApiResponse<PetTypeDto>> GetPetTypeByIdAsync(int id)
        {
            var petType = await _petTypeRepository.GetByIdAsync(id);
            if (petType == null)
            {
                return ApiResponseHelper.FailResponse<PetTypeDto>(
                    404, 
                    new { Message = "Pet type does not exist." }
                );

            }
            return ApiResponseHelper
                .SuccessResponse<PetTypeDto>(200, _mapper.Map<PetTypeDto>(petType));
        }

        public async Task<ApiResponse<PetTypeDto>> PostPetTypeAsync(PetType petType)
        {
            if (await _petTypeRepository.IsTypeNameExits(petType.TypeName))
            {
                return ApiResponseHelper.FailResponse<PetTypeDto>(400, new { TypeName = "Type name already exists." });
            }
            await _petTypeRepository.AddAsync(petType);
            return ApiResponseHelper
                .SuccessResponse<PetTypeDto>(204, null);
        }

        public async Task<ApiResponse<PetTypeDto>> DeletePetTypeAsync(int id)
        {
            if (await _petTypeRepository.GetByIdAsync(id) == null)
            {
                return ApiResponseHelper.FailResponse<PetTypeDto>(404, new { Message = "Pet type not found." });
            }
            await _petTypeRepository.DeleteAsync(id);
            return ApiResponseHelper.SuccessResponse<PetTypeDto>(204, null);
        }
    }
}
