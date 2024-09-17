using AutoMapper;
using FluentValidation;
using MrTakuVetClinic.DTOs.Pet;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Helpers;
using MrTakuVetClinic.Interfaces.Repositories;
using MrTakuVetClinic.Interfaces.Services;
using MrTakuVetClinic.Models;
using System;
using System.Collections.Generic;
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
        private readonly IValidator<PetPostDto> _petValidator;
        private readonly IMapper _mapper;

        public PetService(
            IPetRepository petRepository, 
            IPetTypeRepository petTypeRepository, 
            IUserRepository userRepository, 
            IVisitRepository visitRepository,
            IValidator<PetPostDto> petValidator,
            IMapper mapper)
        {
            _petRepository = petRepository;
            _petTypeRepository = petTypeRepository;
            _userRepository = userRepository;
            _visitRepository = visitRepository;
            _petValidator = petValidator;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<PetDto>>> GetAllPetsAsync()
        {
            return ApiResponseHelper.SuccessResponse<IEnumerable<PetDto>>(
                200,
                (await _petRepository.GetAllPetsAsync())
                .Select(p => _mapper.Map<PetDto>(p)).ToList()
            );
        }

        public async Task<ApiResponse<PaginatedResponse<PetDto>>> GetAllPaginatedPetsAsync(PaginationParameters paginationParams)
        {
            var paginatedPets = await _petRepository.GetPaginatedPetsAsync(paginationParams);
            var paginatedResponse = new PaginatedResponse<PetDto>(
                paginatedPets.Data.Select(p => _mapper.Map<PetDto>(p)),
                paginatedPets.PageNumber,
                paginatedPets.PageSize,
                paginatedPets.TotalItems
            );
            return ApiResponseHelper.SuccessResponse<PaginatedResponse<PetDto>>(200, paginatedResponse);
        }

        public async Task<ApiResponse<PetDto>> GetPetByIdAsync(int id)
        {
            var pet = await _petRepository.GetPetByIdAsync(id);
            if (pet == null)
            {
                return ApiResponseHelper.FailResponse<PetDto>(400, new { Message = "Pet not found." });

            }
            return ApiResponseHelper.SuccessResponse<PetDto>(200, _mapper.Map<PetDto>(pet));
        }

        public async Task<ApiResponse<IEnumerable<PetDto>>> GetUserPetsByUsernameAsync(string username)
        {
            return ApiResponseHelper.SuccessResponse<IEnumerable<PetDto>>(
                200,
                (await _petRepository.GetAllUserPetsAsync(username))
                .Select(p => _mapper.Map<PetDto>(p)).ToList()
            );
        }

        public async Task<ApiResponse<PaginatedResponse<PetDto>>> GetPaginatedUserPetsByUsernameAsync(string username, PaginationParameters paginationParams)
        {
            if (await _userRepository.GetUserByUsernameAsync(username) == null)
            {
                return ApiResponseHelper.FailResponse<PaginatedResponse<PetDto>>(400, new { Message = "User not found." });
            }
            var paginatedPets = await _petRepository.GetAllPaginatedUserPetsAsync(username, paginationParams);
            var paginatedResponse = new PaginatedResponse<PetDto>(
                paginatedPets.Data.Select(p => _mapper.Map<PetDto>(p)),
                paginatedPets.PageNumber,
                paginatedPets.PageSize,
                paginatedPets.TotalItems
            );
            return ApiResponseHelper.SuccessResponse<PaginatedResponse<PetDto>>(200, paginatedResponse);
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
            return ApiResponseHelper.SuccessResponse<PetDto>(200, _mapper.Map<PetDto>(await _petRepository.GetPetByIdAsync(id)));
        }

        public async Task<ApiResponse<PetDto>> PostPetAsync(PetPostDto petPostDto)
        {
            var validationResult = _petValidator.Validate(petPostDto);
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

            var user = await _userRepository.GetUserByUsernameAsync(petPostDto.Username);
            if (user == null)
            {
                return ApiResponseHelper.FailResponse<PetDto>(404, new { Message = "User not found." });

            }
            if (await _petTypeRepository.GetByIdAsync(petPostDto.PetTypeId) == null)
            {
                return ApiResponseHelper.FailResponse<PetDto>(404, new { Message = "Pet type does not exist." });
            }

            var petMapper = (_mapper.Map<Pet>(petPostDto));
            petMapper.UserId = user.UserId;

            var petResponse = await _petRepository.AddAsync(petMapper);
            return ApiResponseHelper.SuccessResponse<PetDto>(201, _mapper.Map<PetDto>(petResponse));
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
            return ApiResponseHelper.SuccessResponse<PetDto>(204, null);
        }
    }
}
