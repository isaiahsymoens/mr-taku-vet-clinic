using MrTakuVetClinic.DTOs.Pet;
using MrTakuVetClinic.DTOs.User;
using MrTakuVetClinic.DTOs.Visit;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        public async Task<IEnumerable<PetDto>> GetAllPetsAsync()
        {
            var pets = await _petRepository.GetAllPetsAsync();
            return pets.Select(p => new PetDto { 
                PetId = p.PetId,
                PetTypeId = p.PetTypeId,
                PetName = p.PetName,
                Breed = p.Breed,
                BirthDate = p.BirthDate,
                User = new UserDto {
                    FirstName = p.User.FirstName,
                    MiddleName = p.User.MiddleName,
                    LastName = p.User.LastName,
                    Email = p.User.Email,
                    Username = p.User.Username,
                    Active = p.User.Active,
                    UserType = p.User.UserType.TypeName
                }
            }).ToList();
        }

        public async Task<PetDto> GetPetByIdAsync(int id)
        {
            var pet = await _petRepository.GetPetByIdAsync(id);
            if (pet == null)
            {
                throw new Exception("Pet not found.");
            }

            return new PetDto {
                PetId = pet.PetId,
                PetTypeId = pet.PetTypeId,
                PetName = pet.PetName,
                Breed = pet.Breed,
                BirthDate = pet.BirthDate,
                User = new UserDto
                {
                    FirstName = pet.User.FirstName,
                    MiddleName = pet.User.MiddleName,
                    LastName = pet.User.LastName,
                    Email = pet.User.Email,
                    Username = pet.User.Username,
                    Active = pet.User.Active,
                    UserType = pet.User.UserType.TypeName
                }
            };
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
            var pet = await _petRepository.GetPetByIdAsync(id);
            if (pet == null)
            {
                throw new ArgumentException("Pet record not found.");
            }

            await _petRepository.DeleteAsync(id);
        }
    }
}
