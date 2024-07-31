using MrTakuVetClinic.DTOs.Pet;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Services
{
    public class PetService
    {
        private readonly IPetRepository _petRepository;
        private readonly IPetTypeRepository _petTypeRepository;
        private readonly IUserRepository _userRepository;

        public PetService(IPetRepository petRepository, IPetTypeRepository petTypeRepository, IUserRepository userRepository)
        {
            _petRepository = petRepository;
            _petTypeRepository = petTypeRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Pet>> GetAllPetsAsync()
        {
            return await _petRepository.GetAllAsync();
        }

        public async Task<Pet> GetPetByIdAsync(int id)
        {
            var pet = await _petRepository.GetPetByIdAsync(id);
            if (pet == null)
            {
                throw new Exception("Pet not found.");
            }
            return pet;
        }

        public async Task UpdatePetByIdAsync(int id, PetUpdateDto petUpdateDto)
        {
            var existingPet = await _petRepository.GetByIdAsync(id);
            if (existingPet == null)
            {
                throw new ArgumentException("Pet record not found.");
            }

            if (petUpdateDto.PetName != null)
            {
                existingPet.PetName = petUpdateDto.PetName;
            }

            if (petUpdateDto.PetTypeId != null)
            {
                existingPet.PetTypeId = petUpdateDto.PetTypeId.Value;
            }

            if (petUpdateDto.Breed != null)
            {
                existingPet.Breed = petUpdateDto.Breed;
            }

            if (petUpdateDto.BirthDate != null)
            {
                existingPet.BirthDate = petUpdateDto.BirthDate;
            }

            await _petRepository.UpdateAsync(existingPet);
        }

        public async Task PostPetAsync(Pet pet)
        {
            if (await _petTypeRepository.GetByIdAsync(pet.PetTypeId) == null)
            {
                throw new ArgumentException("Pet type does not exist.");
            }
            await _petRepository.AddAsync(pet);
        }

        public async Task DeletePetAsync(int id)
        {
            // TODO: refactor & add validation
            await _petRepository.DeleteAsync(id);
        }
    }
}
