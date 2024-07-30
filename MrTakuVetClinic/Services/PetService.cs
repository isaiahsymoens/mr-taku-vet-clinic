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

        public async Task PostPetAsync(PostPetDto pet)
        {
            if (await _petTypeRepository.GetByIdAsync(pet.PetTypeId) == null)
            {
                throw new ArgumentException("Pet type does not exist.");
            }

            var user = await _userRepository.GetUserByUsernameAsync(pet.Username);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            var addPet = new Pet
            {
                PetName = pet.PetName,
                PetTypeId = pet.PetTypeId,
                Breed = pet.Breed,
                BirthDate = pet.BirthDate,
                UserId = user.UserId
            };
            await _petRepository.AddAsync(addPet);
        }

        public async Task DeletePetAsync(int id)
        {
            // TODO: refactor & add validation
            await _petRepository.DeleteAsync(id);
        }
    }
}
