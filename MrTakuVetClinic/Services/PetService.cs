using FluentValidation;
using MrTakuVetClinic.DTOs.Pet;
using MrTakuVetClinic.DTOs.User;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Helpers;
using MrTakuVetClinic.Interfaces.Repositories;
using MrTakuVetClinic.Interfaces.Services;
using MrTakuVetClinic.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepository;
        private readonly IPetTypeRepository _petTypeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVisitRepository _visitRepository;
        private readonly IValidator<Pet> _petValidator;

        public PetService(
            IPetRepository petRepository, 
            IPetTypeRepository petTypeRepository, 
            IUserRepository userRepository, 
            IVisitRepository visitRepository,
            IValidator<Pet> petValidator)
        {
            _petRepository = petRepository;
            _petTypeRepository = petTypeRepository;
            _userRepository = userRepository;
            _visitRepository = visitRepository;
            _petValidator = petValidator;
        }

        public async Task<ApiResponse<PetDto>> GetAllPetsAsync()
        {
            return ApiResponseHelper.SuccessResponse<PetDto>(
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

        public async Task<ApiResponse<PetDto>> GetPetByIdAsync(int id)
        {
            var pet = await _petRepository.GetPetByIdAsync(id);
            if (pet == null)
            {
                return ApiResponseHelper.FailResponse<PetDto>(400, new { Message = "Pet not found." });

            }
            return ApiResponseHelper.SuccessResponse<PetDto>(
                200,
                new PetDto
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
                        Active = pet.User.Active,
                        UserType = pet.User.UserType.TypeName
                    }
                }
            );
        }

        public async Task<ApiResponse<PetDto>> UpdatePetByIdAsync(int id, PetUpdateDto petUpdateDto)
        {
            var existingPet = await _petRepository.GetByIdAsync(id);
            if (existingPet == null)
            {
                return ApiResponseHelper.FailResponse<PetDto>(404, new { Message = "Pet record not found." });
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
            return ApiResponseHelper.SuccessResponse<PetDto>(204, null);
        }

        public async Task<ApiResponse<PetDto>> PostPetAsync(PetPostDto petPostDto)
        {
            if (petPostDto.Username == null)
            {
                return ApiResponseHelper.FailResponse<PetDto>(400, new { Username = "Username is required." });

            }

            var user = await _userRepository.GetUserByUsernameAsync(petPostDto.Username);
            if (user == null)
            {
                return ApiResponseHelper.FailResponse<PetDto>(404, new { Message = "User not found." });

            }

            var pet = new Pet
            {
                UserId = user.UserId,
                PetName = petPostDto.PetName,
                PetTypeId = petPostDto.PetTypeId,
                Breed = petPostDto.Breed,
                BirthDate = petPostDto.BirthDate
            };

            var validationResult = _petValidator.Validate(pet);
            if (!validationResult.IsValid)
            {
                return ApiResponseHelper.FailResponse<PetDto>(
                    400,
                    validationResult.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            e => e.Key,
                            e => e.First().ErrorMessage
                        )
                );
            }

            if (await _petTypeRepository.GetByIdAsync(petPostDto.PetTypeId) == null)
            {
                throw new ArgumentException("Pet type does not exist.");
            }

            var petResponse = await _petRepository.AddAsync(new Pet
            {
                PetName = petPostDto.PetName,
                PetTypeId = petPostDto.PetTypeId,
                Breed = petPostDto.Breed,
                BirthDate = petPostDto.BirthDate,
                UserId = user.UserId
            });

            return ApiResponseHelper.SuccessResponse<PetDto>(
                201,
                new PetDto
                {
                    PetId = petResponse.PetId,
                    PetName = petResponse.PetName,
                    PetType = petResponse.PetType.TypeName,
                    Breed = petResponse.Breed,
                    BirthDate = petResponse.BirthDate,
                    User = new UserDto
                    {
                        FirstName = petResponse.User.FirstName,
                        MiddleName = petResponse.User.MiddleName,
                        LastName = petResponse.User.LastName,
                        Email = petResponse.User.Email,
                        Username = petResponse.User.Username,
                        UserType = petResponse.User.UserType.TypeName,
                        Active = petResponse.User.Active
                    }
                }
            );
        }

        public async Task<ApiResponse<PetDto>> DeletePetAsync(int id)
        { 
            if (await _petRepository.GetPetByIdAsync(id) == null)
            {
                return ApiResponseHelper.FailResponse<PetDto>(404, new { Message = "Pet record not found." });

            }
            if ((await _visitRepository.GetAllVisitsAsync()).FirstOrDefault(p => p.PetId == id) != null)
            {
                return ApiResponseHelper.FailResponse<PetDto>(
                    403, 
                    new { Message = "Cannot delete the pet record because it has associated visit records." }
                );

            }
            await _petRepository.DeleteAsync(id);
            return ApiResponseHelper.SuccessResponse<PetDto>(
                204,
                null
            );
        }
    }
}
