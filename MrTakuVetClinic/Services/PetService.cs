using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces;
using MrTakuVetClinic.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Services
{
    public class PetService
    {
        private readonly IPetRepository _petRepository;
        private readonly IPetTypeRepository _petTypeRepository;

        public PetService(IPetRepository petRepository, IPetTypeRepository petTypeRepository)
        {
            _petRepository = petRepository;
            _petTypeRepository = petTypeRepository;
        }

        public async Task<IEnumerable<Pet>> GetAllPetsAsync()
        {
            return await _petRepository.GetAllAsync();
        }

        public async Task<Pet> GetPetByIdAsync(int id)
        {
            return await _petRepository.GetPetByIdAsync(id);
        }

        public async Task PostPetAsync(Pet pet)
        {
            if (_petTypeRepository.GetByIdAsync(pet.PetTypeId) == null)
            {
                throw new ArgumentException("Pet type does not exist.");
            }

            await _petRepository.AddAsync(pet);

            Console.WriteLine("yey2");
        }
    }
}
