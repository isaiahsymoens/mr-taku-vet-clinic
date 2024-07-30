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

        public UserService(IUserRepository userRepository, IUserTypeRepository userTypeRepository)
        {
            _userRepository = userRepository;
            _userTypeRepository = userTypeRepository;
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

        public async Task<IEnumerable<UserDto>> GetSearchUsersAsync([FromQuery] string firstName, string lastName)
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

        public async Task AddUserAsync(User user)
        {
            if (await _userRepository.IsEmailExits(user.Email))
            {
                throw new ArgumentException("Email is already taken.");
            }

            if (await _userRepository.IsUsernameExits(user.Username))
            {
                throw new ArgumentException("Username is already taken.");
            }

            var userType = await _userTypeRepository.GetByIdAsync(user.UserTypeId);
            if (userType == null)
            {
                throw new ArgumentException("User type does not exist.");
            }
            await _userRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
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
