using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Visit>> GetAllVisitsAsync()
        {
            return await _visitRepository.GetAllAsync();
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

        public async Task PostVisitAsync(Visit visit)
        {
            if (await _petRepository.GetByIdAsync(visit.PetId) != null)
            {
                throw new ArgumentException("Pet does not exist.");
            }

            if (await _visitTypeRepository.GetByIdAsync(visit.VisitTypeId) != null)
            {
                throw new ArgumentException("Visit type does not exist.");
            }

            await _visitRepository.AddAsync(visit);
        }
    }
}
