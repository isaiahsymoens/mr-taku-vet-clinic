using AutoMapper;
using FluentValidation;
using MrTakuVetClinic.DTOs.Visit;
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
    public class VisitService : IVisitService
    {
        private readonly IVisitRepository _visitRepository;
        private readonly IVisitTypeRepository _visitTypeRepository;
        private readonly IPetRepository _petRepository;
        private readonly IValidator<VisitPostDto> _visitValidator;
        private readonly IMapper _mapper;

        public VisitService(
            IVisitRepository visitRepository, 
            IVisitTypeRepository visitTypeRepository, 
            IPetRepository petRepository,
            IValidator<VisitPostDto> visitValidator,
            IMapper mapper)
        {
            _visitRepository = visitRepository;
            _visitTypeRepository = visitTypeRepository;
            _petRepository = petRepository;
            _visitValidator = visitValidator;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<VisitDto>>> GetAllVisitsAsync()
        {   
            return ApiResponseHelper.SuccessResponse<IEnumerable<VisitDto>>(
                200,
                (await _visitRepository.GetAllVisitsAsync())
                .Select(v => _mapper.Map<VisitDto>(v)).ToList()
            );
        }

        public async Task<ApiResponse<PaginatedResponse<VisitDto>>> GetAllPaginatedVisitsAsync(PaginationParameters paginationParams)
        {
            var paginatedVisits = await _visitRepository.GetAllPaginatedVisitsAsync(paginationParams);
            var paginatedResponse = new PaginatedResponse<VisitDto>(
                paginatedVisits.Data.Select(v => _mapper.Map<VisitDto>(v)),
                paginatedVisits.PageNumber,
                paginatedVisits.PageSize,
                paginatedVisits.TotalItems
            );
            return ApiResponseHelper.SuccessResponse<PaginatedResponse<VisitDto>>(200, paginatedResponse);
        }

        public async Task<ApiResponse<VisitDto>> GetVisitById(int id)
        {
            var visit = await _visitRepository.GetVisitByIdAsync(id);
            if (visit == null)
            {
                return ApiResponseHelper.FailResponse<VisitDto>(404, new { Message = "Visit record not found." });
            }
            return ApiResponseHelper.SuccessResponse<VisitDto>(200, _mapper.Map<VisitDto>(visit));
        }

        public async Task<ApiResponse<PaginatedResponse<VisitDto>>> GetPetVisitsByIdAsync(int id, PaginationParameters paginationParams)
        {
            var paginatedVisits = await _visitRepository.GetPetVisitsByIdAsync(id, paginationParams);
            var paginatedResponse = new PaginatedResponse<VisitDto>(
                paginatedVisits.Data.Select(v => _mapper.Map<VisitDto>(v)),
                paginatedVisits.PageNumber,
                paginatedVisits.PageSize,
                paginatedVisits.TotalItems
            );
            return ApiResponseHelper.SuccessResponse<PaginatedResponse<VisitDto>>(200, paginatedResponse);
        }

        public async Task<ApiResponse<PaginatedResponse<VisitDto>>> SearchVisitsAsync(VisitSearchDto visitSearchDto, PaginationParameters paginationParams)
        {
            var paginatedVisits = await _visitRepository.SearchVisitsAsync(visitSearchDto, paginationParams);
            var paginatedResponse = new PaginatedResponse<VisitDto>(
                paginatedVisits.Data.Select(v => _mapper.Map<VisitDto>(v)),
                paginatedVisits.PageNumber,
                paginatedVisits.PageSize,
                paginatedVisits.TotalItems
            );
            return ApiResponseHelper.SuccessResponse<PaginatedResponse<VisitDto>>(200, paginatedResponse);
        }

        public async Task<ApiResponse<VisitDto>> PostVisitAsync(VisitPostDto visitPostDto)
        {
            var validationResult = _visitValidator.Validate(visitPostDto);
            if (!validationResult.IsValid)
            {
                return ApiResponseHelper.FailResponse<VisitDto>(
                    400,
                    validationResult.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            e => e.Key,
                            e => e.First().ErrorMessage
                        )
                );
            }

            if (await _petRepository.GetByIdAsync(visitPostDto.PetId) == null)
            {
                return ApiResponseHelper.FailResponse<VisitDto>(404, new { Message = "Pet does not exist." });
            }
            if (await _visitTypeRepository.GetByIdAsync(visitPostDto.VisitTypeId) == null)
            {
                return ApiResponseHelper.FailResponse<VisitDto>(404, new { Message = "Visit type does not exist." });
            }

            var visitResponse = await _visitRepository.AddAsync(_mapper.Map<Visit>(_mapper.Map<Visit>(visitPostDto)));
            return await GetVisitById(visitResponse.VisitId);
        }

        public async Task<ApiResponse<VisitDto>> UpdatePetByIdAsync(int id, VisitUpdateDto visitUpdateDto)
        {
            var existingVisit = await _visitRepository.GetByIdAsync(id);
            if (existingVisit == null)
            {
                return ApiResponseHelper.FailResponse<VisitDto>(404, new { Message = "Visit record not found." });
            }

            if (visitUpdateDto.VisitTypeId != null)
            {
                existingVisit.VisitTypeId = visitUpdateDto.VisitTypeId.Value;
            }
            if (visitUpdateDto.Date != null)
            { 
                existingVisit.Date = visitUpdateDto.Date;
            }
            if (visitUpdateDto.PetId != null)
            { 
                existingVisit.PetId = visitUpdateDto.PetId.Value;
            }
            if (visitUpdateDto.Notes != null)
            { 
                existingVisit.Notes = visitUpdateDto.Notes;
            }
            await _visitRepository.UpdateAsync(existingVisit);
            var visit = await _visitRepository.GetVisitByIdAsync(id);

            return ApiResponseHelper.SuccessResponse<VisitDto>(200, _mapper.Map<VisitDto>(visit));
        }

        public async Task<ApiResponse<VisitDto>> DeleteVisitAsync(int id)
        {
            if (await _visitRepository.GetByIdAsync(id) == null)
            {
                return ApiResponseHelper.FailResponse<VisitDto>(404, new { Message = "Visit record not found." });
            }
            await _visitRepository.DeleteAsync(id);
            return ApiResponseHelper.SuccessResponse<VisitDto>(204 , null);
        }
    }
}
