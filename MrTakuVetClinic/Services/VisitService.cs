using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.DTOs.Pet;
using MrTakuVetClinic.DTOs.User;
using MrTakuVetClinic.DTOs.Visit;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
            //return (IEnumerable<VisitDto>)await _visitRepository.GetAllVisitsAsync();

            var visits = await _visitRepository.GetAllVisitsAsync();

            return visits.Select(v => new VisitDto
            {
                Date = v.Date,
                PetId = v.PetId,
                Pet = new PetDto
                {
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

        public async Task<Visit> GetVisitById(int id)
        {
            var visit = await _visitRepository.GetByIdAsync(id);
            if (visit == null)
            {
                throw new ArgumentException("Visit not found.");
            }

            return visit;
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
