using AutoMapper;
using FluentValidation;
using MrTakuVetClinic.DTOs.VisitType;
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
    public class VisitTypeService : IVisitTypeService
    {
        private readonly IVisitTypeRepository _visitTypeRepository;
        private readonly IValidator<VisitTypePostDto> _visitTypeValidator;
        private readonly IMapper _mapper;

        public VisitTypeService(
            IVisitTypeRepository visitTypeRepository,
            IValidator<VisitTypePostDto> visitTypeValidator,
            IMapper mapper)
        {
            _visitTypeRepository = visitTypeRepository;
            _visitTypeValidator = visitTypeValidator;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<VisitTypeDto>>> GetAllVisitTypesAsync()
        {
            return ApiResponseHelper
                .SuccessResponse<IEnumerable<VisitTypeDto>>(
                    200, 
                    (await _visitTypeRepository.GetAllAsync())
                    .Select(v => _mapper.Map<VisitTypeDto>(v))
                );
        }

        public async Task<ApiResponse<VisitTypeDto>> GetVisitTypeByIdAsync(int id)
        {
            var visitType = await _visitTypeRepository.GetByIdAsync(id);
            if (visitType == null)
            {
                return ApiResponseHelper.FailResponse<VisitTypeDto>(
                    404, 
                    new { Message = "Visit type does not exist." }
                );
            }
            return ApiResponseHelper
                .SuccessResponse<VisitTypeDto>(200, _mapper.Map<VisitTypeDto>(visitType));
        }

        public async Task<ApiResponse<VisitTypeDto>> PostVisitTypeAsync(VisitTypePostDto visitTypePostDto)
        {
            if (await _visitTypeRepository.IsTypeNameExits(visitTypePostDto.TypeName))
            {
                return ApiResponseHelper.FailResponse<VisitTypeDto>(400, new { TypeName = "Type name already exists." });
            }
            return ApiResponseHelper.SuccessResponse<VisitTypeDto>(
                201,
                _mapper.Map<VisitTypeDto>(await _visitTypeRepository.AddAsync(_mapper.Map<VisitType>(visitTypePostDto)))
            );
        }

        public async Task<ApiResponse<VisitTypeDto>> UpdateVisitTypeAsync(int id, VisitTypeUpdateDto visitTypeUpdateDto)
        {
            var existingVisitType = await _visitTypeRepository.GetByIdAsync(id);
            if (existingVisitType == null)
            {
                return ApiResponseHelper.FailResponse<VisitTypeDto>(404, new { Message = "Visit type not found." });
            }
            if (visitTypeUpdateDto.TypeName == null)
            {
                return ApiResponseHelper.FailResponse<VisitTypeDto>(400, new { Message = "No changes detected." });
            }
            existingVisitType.TypeName = visitTypeUpdateDto.TypeName;
            await _visitTypeRepository.UpdateAsync(existingVisitType);
            return ApiResponseHelper.SuccessResponse<VisitTypeDto>(204, null);
        }

        public async Task<ApiResponse<VisitTypeDto>> DeleteVisitTypeAsync(int id)
        {
            if (await _visitTypeRepository.GetByIdAsync(id) == null)
            {
                return ApiResponseHelper.FailResponse<VisitTypeDto>(404, new { Message = "Visit type not found." });
            }
            await _visitTypeRepository.DeleteAsync(id);
            return ApiResponseHelper.SuccessResponse<VisitTypeDto>(204, null);
        }
    }
}
