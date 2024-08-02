using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.DTOs.User;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var user = await _userRepository.GetAllUsersAsync();
            return user.Select(u => new UserDto
            {
                FirstName = u.FirstName,
                MiddleName = u.MiddleName,
                LastName = u.LastName,
                Email = u.Email,
                Username = u.Username,
                UserType = u.UserType.TypeName,
                Active = u.Active
            });
        }

        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }
            return new UserDto 
            {
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                UserType = user.UserType.TypeName,
                Active = user.Active
            };
        }

        public async Task<IEnumerable<UserDto>> GetSearchUsersAsync([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var users = await _userRepository.GetSearchUsersAsync(firstName, lastName);
            return users.Select(u => new UserDto
            {
                FirstName = u.FirstName,
                MiddleName = u.MiddleName,
                LastName = u.LastName,
                Email = u.Email,
                Username = u.Username,
                UserType = u.UserType.TypeName,
                Active = u.Active
            });
        }

        public async Task<UserDto> PostUserAsync(UserPostDto userPostDto)
        {
            if (await _userRepository.IsEmailExits(userPostDto.Email))
            {
                throw new ArgumentException("Email is already taken.");
            }

            if (await _userRepository.IsUsernameExits(userPostDto.Username))
            {
                throw new ArgumentException("Username is already taken.");
            }

            if (await _userTypeRepository.GetByIdAsync(userPostDto.UserTypeId) == null)
            {
                throw new ArgumentException("User type does not exist.");
            }

            var convertToUserEntity = new User
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

            //var userValidator = _userValidator.Validate(convertToUserEntity);
            //if (!userValidator.IsValid)
            //{
            //    return userValidator;
            //}

            var user = await _userRepository.AddAsync(convertToUserEntity);

            return new UserDto
            {
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                UserType = user.UserType.TypeName,
                Active = user.Active
            };
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

            //foreach (var property in userUpdateDto.GetType().GetProperties())
            //{
            //    var newvalue = property.GetValue(userUpdateDto, null);
            //    if (newvalue != null)
            //    {
            //        var existingproperty = userUpdateDto.GetType().GetProperty(property.Name);
            //        if (existingproperty != null && existingproperty.CanWrite)
            //        {
            //            existingproperty.SetValue(existingUser, newvalue, null);
            //        }
            //    }
            //}

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
