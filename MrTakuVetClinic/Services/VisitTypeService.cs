﻿using AutoMapper;
using FluentValidation;
using MrTakuVetClinic.DTOs.VisitType;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Helpers;
using MrTakuVetClinic.Interfaces.Repositories;
using MrTakuVetClinic.Interfaces.Services;
using MrTakuVetClinic.Models;
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

        public async Task<ApiResponse<VisitTypeDto>> GetAllVisitTypesAsync()
        {
            return ApiResponseHelper
                .SuccessResponse<VisitTypeDto>(
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

        public async Task<ApiResponse<VisitType>> PostVisitTypeAsync(VisitType visitType)
        {
            await _visitTypeRepository.AddAsync(visitType);
            return ApiResponseHelper
                .SuccessResponse<VisitType>(204, null);
        }
    }
}
