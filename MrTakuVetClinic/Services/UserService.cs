using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using MrTakuVetClinic.DTOs.User;
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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserTypeRepository _userTypeRepository;
        private readonly IValidator<UserPostDto> _userValidator;
        private readonly IMapper _mapper;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(
            IUserRepository userRepository, 
            IUserTypeRepository userTypeRepository, 
            IValidator<UserPostDto> userValidator,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _userTypeRepository = userTypeRepository;
            _userValidator = userValidator;
            _mapper = mapper;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<ApiResponse<PaginatedResponse<UserDto>>> GetAllPaginatedUsersAsync(PaginationParameters paginationParams, UserSortDto userSortDto)
        {
            var paginatedUsers = await _userRepository.GetPaginatedUsersAsync(paginationParams, userSortDto);
            var paginatedResponse = new PaginatedResponse<UserDto>(
                paginatedUsers
                    .Data
                    .Select(u => _mapper.Map<UserDto>(u)), 
                paginatedUsers.PageNumber, 
                paginatedUsers.PageSize, 
                paginatedUsers.TotalItems
            );
            return ApiResponseHelper.SuccessResponse<PaginatedResponse<UserDto>>(200, paginatedResponse);
        }

        public async Task<ApiResponse<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            return ApiResponseHelper.SuccessResponse<IEnumerable<UserDto>> (
                200,
                (await _userRepository.GetAllUsersAsync())
                .Select(u => _mapper.Map<UserDto>(u))
            );
        }

        public async Task<ApiResponse<UserDto>> GetUserByUsernameAsync(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return ApiResponseHelper.FailResponse<UserDto>(404, new { Message = "User not found." });
            }
            return ApiResponseHelper.SuccessResponse<UserDto>(200, _mapper.Map<UserDto>(user));
        }

        public async Task<ApiResponse<PaginatedResponse<UserDto>>> GetSearchUsersAsync(UserSearchDto userSearchDto, UserSortDto userSortDto)
        {
            var paginatedUsers = await _userRepository.GetSearchUsersAsync(userSearchDto, userSortDto);
            var paginatedResponse = new PaginatedResponse<UserDto>(
                paginatedUsers
                    .Data
                    .Select(u => _mapper.Map<UserDto>(u)),
                paginatedUsers.PageNumber,
                paginatedUsers.PageSize,
                paginatedUsers.TotalItems
            );
            return ApiResponseHelper.SuccessResponse<PaginatedResponse<UserDto>>(200, paginatedResponse);
        }

        public async Task<ApiResponse<UserPassword>> GetUserPasswordByUsernameAsync(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return ApiResponseHelper.FailResponse<UserPassword>(404, new { Message = "User not found." });
            }
            return ApiResponseHelper.SuccessResponse<UserPassword>(200, _mapper.Map<UserPassword>(user));
        }

        public async Task<ApiResponse<UserDto>> PostUserAsync(UserPostDto userPostDto)
        {
            var validationResult = _userValidator.Validate(userPostDto);
            if (!validationResult.IsValid)
            {
                return ApiResponseHelper.FailResponse<UserDto>(
                    400, 
                    validationResult.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            e => e.Key,
                            e => e.First().ErrorMessage
                        )
                );
            }

            var errors = new Dictionary<string, string>();
            if (await _userRepository.IsEmailExits(userPostDto.Email))
            {
                errors["Email"] = "Email is already taken.";
            }
            if (await _userRepository.IsUsernameExits(userPostDto.Username))
            {
                errors["Username"] = "Username is already taken.";
            }
            if (await _userTypeRepository.GetByIdAsync(userPostDto.UserTypeId) == null)
            {
                errors["UserType"] = "User type does not exist.";
            }

            if (errors.Any())
            {
                return ApiResponseHelper.FailResponse<UserDto>(404, errors);
            }

            var user = _mapper.Map<User>(userPostDto);
            user.Password = _passwordHasher.HashPassword(user, userPostDto.Password);

            var userResponse = await _userRepository.AddAsync(user);
            return ApiResponseHelper.SuccessResponse<UserDto>(
                201,
                _mapper.Map<UserDto>(userResponse)
            );
        }

         public async Task<ApiResponse<UserDto>> PostLoginUserAsync(UserLoginDto userLoginDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(userLoginDto.UserName);
            if (user == null)
            {
                return ApiResponseHelper.FailResponse<UserDto>(400, new { Username = "Couldn't find your Account." });
            }

            var userPassword = await GetUserPasswordByUsernameAsync(userLoginDto.UserName);
            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, userPassword.Data.Password, userLoginDto.Password);

            if (passwordVerificationResult != PasswordVerificationResult.Success)
            {
                return ApiResponseHelper.FailResponse<UserDto>(400, new { Password = "Incorrect password." });
            }

            return ApiResponseHelper.SuccessResponse<UserDto>(201, _mapper.Map<UserDto>(user));
        }

        public async Task<ApiResponse<UserDto>> UpdateUserAsync(String username, UserUpdateDto userUpdateDto)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(username);
            if (existingUser == null)
            {
                return ApiResponseHelper.FailResponse<UserDto>(400, new { Message = "User not found." });
            }

            var errors = new Dictionary<string, string>();
            if (userUpdateDto.FirstName != null)
            {
                existingUser.FirstName = userUpdateDto.FirstName;
            }
            if (userUpdateDto.MiddleName != null)
            {
                existingUser.MiddleName = userUpdateDto.MiddleName;
            }
            if (userUpdateDto.LastName != null)
            {
                existingUser.LastName = userUpdateDto.LastName;
            }
            if (userUpdateDto.Email != null)
            {
                if (await _userRepository.IsEmailExits(userUpdateDto.Email))
                {
                    errors["Email"] = "Email is already taken.";
                }
                existingUser.Email = userUpdateDto.Email;
            }
            if (userUpdateDto.Password != null)
            {
                var passwordVerification = _passwordHasher.VerifyHashedPassword(existingUser, existingUser.Password, userUpdateDto.Password);

                if (passwordVerification == PasswordVerificationResult.Success)
                {
                    errors["Password"] = "New password cannot be the same as the old password.";
                }
                else
                {
                    existingUser.Password = userUpdateDto.Password;
                }
            }
            if (userUpdateDto.Username != null)
            {
                if (await _userRepository.IsUsernameExits(userUpdateDto.Username))
                {
                    errors["Username"] = "Username is already taken.";
                }
                existingUser.Username = userUpdateDto.Username;
            }
            if (userUpdateDto.UserTypeId != null)
            {
                existingUser.UserTypeId = userUpdateDto.UserTypeId.Value;
            }
            if (userUpdateDto.Active != null)
            {
                existingUser.Active = userUpdateDto.Active.Value;
            }

            if (errors.Any())
            {
                return ApiResponseHelper.FailResponse<UserDto>(400, errors);
            }

            var validationResult = _userValidator.Validate(_mapper.Map<UserPostDto>(existingUser));
            if (!validationResult.IsValid)
            {
                return ApiResponseHelper.FailResponse<UserDto>(
                    400,
                    validationResult.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            e => e.Key,
                            e => e.First().ErrorMessage
                        )
                );
            }

            if (userUpdateDto.Password != null)
            {
                existingUser.Password = _passwordHasher.HashPassword(existingUser, userUpdateDto.Password);
            }

            await _userRepository.UpdateAsync(existingUser);
            return ApiResponseHelper.SuccessResponse<UserDto>
                (200, _mapper.Map<UserDto>(await _userRepository.GetUserByUsernameAsync(existingUser.Username)));
        }

        public async Task<ApiResponse<UserDto>> DeleteUserByUsernameAsync(string username)
        {
            if (await _userRepository.GetUserByUsernameAsync(username) == null)
            {
                return ApiResponseHelper.FailResponse<UserDto>(400, new { Message = "User not found." });
            }
            await _userRepository.DeleteUserByUsernameAsync(username);
            return ApiResponseHelper.SuccessResponse<UserDto>(204, null);
        }
    }
}
