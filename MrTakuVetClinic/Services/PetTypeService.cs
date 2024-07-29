using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces;
using System;
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
            var pet = await _petTypeRepository.GetByIdAsync(id);
            if (pet == null)
            {
                throw new ArgumentException("Pet type does not exist.");
            }
            return pet;
        }

        public async Task PostPetTypeAsync(PetType petType)
        {
            await _petTypeRepository.AddAsync(petType);
        }
    }
}
