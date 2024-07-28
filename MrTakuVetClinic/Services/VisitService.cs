using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Services
{
    public class VisitService
    {
        private readonly IVisitRepository _visitRepository;

        public VisitService(IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository;
        }

        public async Task<IEnumerable<Visit>> GetAllVisitsAsync()
        {
            return await _visitRepository.GetAllAsync();
        }
    }
}
