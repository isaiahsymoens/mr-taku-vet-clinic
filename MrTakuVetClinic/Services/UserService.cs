﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.DTOs.User;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Helpers;
using MrTakuVetClinic.Interfaces;
using MrTakuVetClinic.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserTypeRepository _userTypeRepository;
        private readonly IValidator<User> _userValidator;

        public UserService(IUserRepository userRepository, IUserTypeRepository userTypeRepository, IValidator<User> userValidator)
        {
            _userRepository = userRepository;
            _userTypeRepository = userTypeRepository;
            _userValidator = userValidator;
        }

        public async Task<ApiResponse<UserDto>> GetAllUsersAsync()
        {
            return ApiResponseHelper.SuccessResponse<UserDto>(
                200,
                (await _userRepository.GetAllUsersAsync())
                .Select(u => new UserDto
                {
                    FirstName = u.FirstName,
                    MiddleName = u.MiddleName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Username = u.Username,
                    UserType = u.UserType.TypeName,
                    Active = u.Active
                })
            );
        }

        public async Task<ApiResponse<UserDto>> GetUserByUsernameAsync(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return ApiResponseHelper.FailResponse<UserDto>(404, new { Message = "User not found." });
            }
            return ApiResponseHelper.SuccessResponse<UserDto>(
                200,
                new UserDto 
                {
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Username = user.Username,
                    UserType = user.UserType.TypeName,
                    Active = user.Active
                }
            );
        }

        public async Task<ApiResponse<UserDto>> GetSearchUsersAsync([FromQuery] string firstName, [FromQuery] string lastName)
        {
            return ApiResponseHelper.SuccessResponse<UserDto>(
                200,
                (await _userRepository.GetSearchUsersAsync(firstName, lastName))
                .Select(u => new UserDto
                {
                    FirstName = u.FirstName,
                    MiddleName = u.MiddleName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Username = u.Username,
                    UserType = u.UserType.TypeName,
                    Active = u.Active
                })
            );
        }

        public async Task<ApiResponse<UserDto>> PostUserAsync(UserPostDto userPostDto)
        {
            var user = new User
            {
                FirstName = userPostDto.FirstName,
                MiddleName = userPostDto.MiddleName,
                LastName = userPostDto.LastName,
                Email = userPostDto.Email,
                Password = userPostDto.Password,
                Username = userPostDto.Username,
                UserTypeId = userPostDto.UserTypeId,
                Active = userPostDto.Active
            };

            var validationResult = _userValidator.Validate(user);
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

            if (await _userRepository.IsEmailExits(user.Email))
            {
                return ApiResponseHelper.FailResponse<UserDto>(400, new { Email = "Email is already taken." });
            }

            if (await _userRepository.IsUsernameExits(user.Username))
            {
                return ApiResponseHelper.FailResponse<UserDto>(400, new { Username = "Username is already taken." });
            }

            if (await _userTypeRepository.GetByIdAsync(user.UserTypeId) == null)
            {
                return ApiResponseHelper.FailResponse<UserDto>(400, new { Message = "User type does not exist." });
            }

            var userResponse = await _userRepository.AddAsync(user);
            return ApiResponseHelper.SuccessResponse<UserDto>(
                201, 
                new UserDto
                {
                    FirstName = userResponse.FirstName,
                    MiddleName = userResponse.MiddleName,
                    LastName = userResponse.LastName,
                    Email = userResponse.Email,
                    Username = userResponse.Username,
                    UserType = userResponse.UserType.TypeName,
                    Active = userResponse.Active
                }
            );
        }

        public async Task UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(userUpdateDto.Username);

            if (existingUser == null)
            {
                throw new ArgumentException("User not found.");
            }

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
                    throw new ArgumentException("Email is already taken.");
                }
                existingUser.Email = userUpdateDto.Email;
            }

            if (userUpdateDto.Password != null)
            {
                existingUser.Password = userUpdateDto.Password;
            }

            if (userUpdateDto.Username != null)
            {
                if (await _userRepository.IsUsernameExits(userUpdateDto.Username))
                {
                    throw new ArgumentException("Username is already taken.");
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

            await _userRepository.UpdateAsync(existingUser);
        }

        public async Task DeleteUserByUsernameAsync(string username)
        {
            if (await _userRepository.GetUserByUsernameAsync(username) == null)
            {
                throw new ArgumentException("User not found.");
            }
            await _userRepository.DeleteUserByUsernameAsync(username);
        }
    }
}
