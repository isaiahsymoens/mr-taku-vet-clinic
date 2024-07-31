using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.DTOs.Pet;
using MrTakuVetClinic.DTOs.User;
using MrTakuVetClinic.DTOs.Visit;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces;
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

        public VisitService(IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository;
        }

        public async Task<IEnumerable<VisitDto>> GetAllVisitsAsync()
        {
            var visits = await _visitRepository.GetAllVisitsAsync();
            return visits.Select(v => new VisitDto
            {
                VisitId = v.VisitId,
                Date = v.Date,
                PetId = v.PetId,
                Pet = new PetDto
                {
                    PetId = v.Pet.PetId,
                    PetName = v.Pet.PetName,
                    PetTypeId = v.Pet.PetTypeId,
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

        public async Task<VisitDto> GetVisitById(int id)
        {
            // TODO: Temporary fix
            //var visit = await _visitRepository.GetVisitByIdAsync(id);
            var visits = await _visitRepository.GetAllVisitsAsync();
            var visit = visits.FirstOrDefault(v => v.VisitId == id);
            if (visit == null)
            {
                throw new ArgumentException("Visit record not found.");
            }
            return new VisitDto
            {
                Date = visit.Date,
                PetId = visit.PetId,
                Pet = new PetDto
                {
                    PetId = visit.PetId,
                    PetName = visit.Pet.PetName,
                    PetTypeId = visit.Pet.PetTypeId,
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
            };
        }

        public async Task<IEnumerable<Visit>> SearchVisitsAsync([FromQuery] VisitFilterDto visitFilterDto)
        {
            var visits = await _visitRepository.SearchVisitsAsync(visitFilterDto);
            Console.WriteLine("##################################################");
            return visits;
            //Console.WriteLine(visits);
        }

        public async Task PostVisitAsync(Visit visit)
        {
            // TODO: Add validation to check if the pet and visit type exists before saving the data.
            //if (await _petRepository.GetByIdAsync(visit.PetId) != null)
            //{
            //    throw new ArgumentException("Pet does not exist.");
            //}

            //if (await _visitTypeRepository.GetByIdAsync(visit.VisitTypeId) != null)
            //{
            //    throw new ArgumentException("Visit type does not exist.");
            //}

            await _visitRepository.AddAsync(visit);
        }
    }
}
