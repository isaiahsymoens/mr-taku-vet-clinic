using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.DTOs.Pet;
using MrTakuVetClinic.DTOs.User;
using MrTakuVetClinic.DTOs.Visit;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Helpers;
using MrTakuVetClinic.Interfaces;
using MrTakuVetClinic.Interfaces.Services;
using MrTakuVetClinic.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Services
{
    public class VisitService : IVisitService
    {
        private readonly IVisitRepository _visitRepository;
        private readonly IVisitTypeRepository _visitTypeRepository;
        private readonly IPetRepository _petRepository;
        private readonly IValidator<Visit> _visitValidator;

        public VisitService(
            IVisitRepository visitRepository, 
            IVisitTypeRepository visitTypeRepository, 
            IPetRepository petRepository,
            IValidator<Visit> visitValidator)
        {
            _visitRepository = visitRepository;
            _visitTypeRepository = visitTypeRepository;
            _petRepository = petRepository;
            _visitValidator = visitValidator;
        }

        public async Task<ApiResponse<VisitDto>> GetAllVisitsAsync()
        {   
            return ApiResponseHelper.SuccessResponse<VisitDto>(
                200,
                (await _visitRepository.GetAllVisitsAsync())
                .Select(v => new VisitDto
                {
                    VisitId = v.VisitId,
                    VisitType = v.VisitType.TypeName,
                    Date = v.Date,
                    PetId = v.PetId,
                    Pet = new PetDto
                    {
                        PetId = v.Pet.PetId,
                        PetName = v.Pet.PetName,
                        Breed = v.Pet.Breed,
                        BirthDate = v.Pet.BirthDate,
                        User = new UserDto
                        {
                            FirstName = v.Pet.User.FirstName,
                            MiddleName = v.Pet.User.MiddleName,
                            LastName = v.Pet.User.LastName,
                            Email = v.Pet.User.Email,
                            Username = v.Pet.User.Username,
                            Active = v.Pet.User.Active,
                            UserType = v.Pet.User.UserType.TypeName
                        }
                    }
                }).ToList()
            );
        }

        public async Task<ApiResponse<VisitDto>> GetVisitById(int id)
        {
            var visit = await _visitRepository.GetVisitByIdAsync(id);
            if (visit == null)
            {
                return ApiResponseHelper.FailResponse<VisitDto>(404, new { Message = "Visit record not found." });
            }
            return ApiResponseHelper.SuccessResponse<VisitDto>(
                200,
                new VisitDto
                {
                    VisitId = visit.VisitId,
                    VisitType = visit.VisitType.TypeName,
                    Date = visit.Date,
                    PetId = visit.PetId,
                    Pet = new PetDto
                    {
                        PetId = visit.PetId,
                        PetName = visit.Pet.PetName,
                        Breed = visit.Pet.Breed,
                        BirthDate = visit.Pet.BirthDate,
                        User = new UserDto
                        {
                            FirstName = visit.Pet.User.FirstName,
                            MiddleName = visit.Pet.User.MiddleName,
                            LastName = visit.Pet.User.LastName,
                            Email = visit.Pet.User.Email,
                            Username = visit.Pet.User.Username,
                            Active = visit.Pet.User.Active,
                            UserType = visit.Pet.User.UserType.TypeName
                        }
                    }
                }
            );
        }

        public async Task<ApiResponse<VisitDto>> SearchVisitsAsync([FromQuery] VisitFilterDto visitFilterDto)
        {
            return ApiResponseHelper.SuccessResponse<VisitDto>(
                200,
                (await _visitRepository.SearchVisitsAsync(visitFilterDto))
                .Select(v => new VisitDto
                {
                    VisitId = v.VisitId,
                    VisitType = v.VisitType.TypeName,
                    Date = v.Date,
                    PetId = v.PetId,
                    Pet = new PetDto
                    {
                        PetId = v.Pet.PetId,
                        PetName = v.Pet.PetName,
                        Breed = v.Pet.Breed,
                        BirthDate = v.Pet.BirthDate,
                        User = new UserDto
                        {
                            FirstName = v.Pet.User.FirstName,
                            MiddleName = v.Pet.User.MiddleName,
                            LastName = v.Pet.User.LastName,
                            Email = v.Pet.User.Email,
                            Username = v.Pet.User.Username,
                            Active = v.Pet.User.Active,
                            UserType = v.Pet.User.UserType.TypeName
                        }
                    }
                }).ToList()
            );
        }

        public async Task<ApiResponse<VisitDto>> PostVisitAsync(VisitPostDto visitPostDto)
        {
            var visit = new Visit
            {
                VisitTypeId = visitPostDto.VisitTypeId,
                Date = visitPostDto.Date,
                PetId = visitPostDto.PetId,
                Notes = visitPostDto.Notes,
            };

            var validationResult = _visitValidator.Validate(visit);
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

            var visitResponse = await _visitRepository.AddAsync(new Visit
            { 
                VisitTypeId = visitPostDto.VisitTypeId,
                PetId = visitPostDto.PetId,
                Date = visitPostDto.Date,
                Notes = visitPostDto.Notes
            });

            // TODO: Temporary fix to get visit complete details
            return await GetVisitById(visitResponse.VisitId);
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
