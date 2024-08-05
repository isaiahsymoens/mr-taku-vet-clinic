using MrTakuVetClinic.DTOs.Pet;
using MrTakuVetClinic.DTOs.User;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Helpers;
using MrTakuVetClinic.Interfaces;
using MrTakuVetClinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Services
{
    public class PetService
    {
        private readonly IPetRepository _petRepository;
        private readonly IPetTypeRepository _petTypeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVisitRepository _visitRepository;

        public PetService(IPetRepository petRepository, IPetTypeRepository petTypeRepository, IUserRepository userRepository, IVisitRepository visitRepository)
        {
            _petRepository = petRepository;
            _petTypeRepository = petTypeRepository;
            _userRepository = userRepository;
            _visitRepository = visitRepository;
        }

        public async Task<ApiResponse<UserDto>> GetAllPetsAsync()
        {
            return ApiResponseHelper.SuccessResponse<UserDto>(
                200,
                (await _petRepository.GetAllPetsAsync())
                .Select(p => new PetDto
                {
                    PetId = p.PetId,
                    PetName = p.PetName,
                    PetType = p.PetType.TypeName,
                    Breed = p.Breed,
                    BirthDate = p.BirthDate,
                    User = new UserDto
                    {
                        FirstName = p.User.FirstName,
                        MiddleName = p.User.MiddleName,
                        LastName = p.User.LastName,
                        Email = p.User.Email,
                        Username = p.User.Username,
                        Active = p.User.Active,
                        UserType = p.User.UserType.TypeName
                    }
                }).ToList()
            );
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
                PetName = pet.PetName,
                PetType = pet.PetType.TypeName,
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

        public async Task<PetDto> PostPetAsync(PetPostDto petPostDto)
        {
            if (petPostDto.Username == null)
            {
                throw new ArgumentException("Username is required.");
            }

            var user = await _userRepository.GetUserByUsernameAsync(petPostDto.Username);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            if (await _petTypeRepository.GetByIdAsync(petPostDto.PetTypeId) == null)
            {
                throw new ArgumentException("Pet type does not exist.");
            }

            var pet = await _petRepository.AddAsync(new Pet
            {
                PetName = petPostDto.PetName,
                PetTypeId = petPostDto.PetTypeId,
                Breed = petPostDto.Breed,
                BirthDate = petPostDto.BirthDate,
                UserId = user.UserId
            });

            return new PetDto
            {
                PetId = pet.PetId,
                PetName = pet.PetName,
                PetType = pet.PetType.TypeName,
                Breed = pet.Breed,
                BirthDate = pet.BirthDate,
                User = new UserDto
                {
                    FirstName = pet.User.FirstName,
                    MiddleName = pet.User.MiddleName,
                    LastName = pet.User.LastName,
                    Email = pet.User.Email,
                    Username = pet.User.Username,
                    UserType = pet.User.UserType.TypeName,
                    Active = pet.User.Active
                }
            };
        }

        public async Task DeletePetAsync(int id)
        { 
            var pet = await _petRepository.GetPetByIdAsync(id);
            if (pet == null)
            {
                throw new ArgumentException("Pet record not found.");
            }

            var visits = await _visitRepository.GetAllVisitsAsync();
            if (visits.FirstOrDefault(p => p.PetId == id) != null)
            {
                throw new ArgumentException("Cannot delete the pet record because it has associated visit records.");
            }
            
            await _petRepository.DeleteAsync(id);
        }
    }
}
