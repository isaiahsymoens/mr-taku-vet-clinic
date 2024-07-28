using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Services
{
    public class PetTypeService
    {
        private readonly IPetTypeRepository _petTypeRepository;
        public PetTypeService(IPetTypeRepository petTypeRepository)
        {
            _petTypeRepository = petTypeRepository;
        }

        public async Task<IEnumerable<PetType>> GetAllPetTypesAsync()
        {
            return await _petTypeRepository.GetAllAsync();
        }

        public async Task<PetType> GetPetTypeByIdAsync(int id)
        {
            return await _petTypeRepository.GetByIdAsync(id);
        }

        public async Task PostPetTypeAsync(PetType petType)
        {
            await _petTypeRepository.AddAsync(petType);
        }
    }
}
