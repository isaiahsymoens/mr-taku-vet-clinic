using AutoMapper;
using FluentValidation;
using MrTakuVetClinic.DTOs.PetType;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Helpers;
using MrTakuVetClinic.Interfaces.Repositories;
using MrTakuVetClinic.Interfaces.Services;
using MrTakuVetClinic.Models;
using System.Collections.Generic;
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

        public async Task<ApiResponse<IEnumerable<PetTypeDto>>> GetAllPetTypesAsync()
        {
            return ApiResponseHelper
                .SuccessResponse<IEnumerable<PetTypeDto>>(
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

        public async Task<ApiResponse<PetTypeDto>> PostPetTypeAsync(PetTypePostDto petTypePostDto)
        {
            if (await _petTypeRepository.IsTypeNameExits(petTypePostDto.TypeName))
            {
                return ApiResponseHelper.FailResponse<PetTypeDto>(400, new { TypeName = "Type name already exists." });
            }
            return ApiResponseHelper.SuccessResponse<PetTypeDto>(
                201, 
                _mapper.Map<PetTypeDto>(await _petTypeRepository.AddAsync(_mapper.Map<PetType>(petTypePostDto)))    
            );
        }

        public async Task<ApiResponse<PetTypeDto>> UpdatePetTypeAsync(int id, PetTypeUpdateDto petTypeUpdateDto)
        {
            var existingPetType = await _petTypeRepository.GetByIdAsync(id);
            if (existingPetType == null)
            {
                return ApiResponseHelper.FailResponse<PetTypeDto>(404, new { Message = "Pet type not found." });
            }
            if (petTypeUpdateDto.TypeName == null)
            {
                return ApiResponseHelper.FailResponse<PetTypeDto>(400, new { Message = "No changes detected." });
            }
            existingPetType.TypeName = petTypeUpdateDto.TypeName;
            await _petTypeRepository.UpdateAsync(existingPetType);
            return ApiResponseHelper.SuccessResponse<PetTypeDto>(204, null);
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
