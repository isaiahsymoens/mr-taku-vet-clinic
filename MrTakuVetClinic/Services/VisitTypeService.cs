using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces;
using MrTakuVetClinic.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Services
{
    public class VisitTypeService
    {
        private readonly IVisitTypeRepository _visitTypeRepository;

        public VisitTypeService(IVisitTypeRepository visitTypeRepository)
        {
            _visitTypeRepository = visitTypeRepository;
        }

        public async Task<IEnumerable<VisitType>> GetAllVisitTypesAsync()
        {
            return await _visitTypeRepository.GetAllAsync();
        }

        public async Task<VisitType> GetVisitTypeByIdAsync(int id)
        {
            return await _visitTypeRepository.GetByIdAsync(id);
        }

        public async Task PostVisitTypeAsync(VisitType visitType)
        {
            await _visitTypeRepository.AddAsync(visitType);
        }
    }
}
