using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.DTOs.Pet;
using MrTakuVetClinic.DTOs.User;
using MrTakuVetClinic.DTOs.Visit;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Helpers;
using MrTakuVetClinic.Interfaces;
using MrTakuVetClinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Services
{
    public class VisitService
    {
        private readonly IVisitRepository _visitRepository;
        private readonly IVisitTypeRepository _visitTypeRepository;
        private readonly IPetRepository _petRepository;

        public VisitService(IVisitRepository visitRepository, IVisitTypeRepository visitTypeRepository, IPetRepository petRepository)
        {
            _visitRepository = visitRepository;
            _visitTypeRepository = visitTypeRepository;
            _petRepository = petRepository;

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

        public async Task<IEnumerable<VisitDto>> SearchVisitsAsync([FromQuery] VisitFilterDto visitFilterDto)
        {
            var visits = await _visitRepository.SearchVisitsAsync(visitFilterDto);
            return visits.Select(v => new VisitDto
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
            }).ToList();
        }

        public async Task<VisitDto> PostVisitAsync(VisitPostDto visitPostDto)
        {
            if (await _petRepository.GetByIdAsync(visitPostDto.PetId) == null)
            {
                throw new ArgumentException("Pet does not exist.");
            }
            if (await _visitTypeRepository.GetByIdAsync(visitPostDto.VisitTypeId) == null)
            {
                throw new ArgumentException("Visit type does not exist.");
            }

            var visit = await _visitRepository.AddAsync(new Visit
            { 
                VisitTypeId = visitPostDto.VisitTypeId,
                PetId = visitPostDto.PetId,
                Date = visitPostDto.Date,
                Notes = visitPostDto.Notes
            });

            // TODO: Temporary fix to get visit complete details
            //return await GetVisitById(visit.VisitId);
            return null;
        }

        public async Task DeleteVisitAsync(int id)
        {
            var visit = await _visitRepository.GetByIdAsync(id);
            if (visit == null)
            {
                throw new ArgumentException("Visit record not found.");
            }
            await _visitRepository.DeleteAsync(id);
        }
    }
}
