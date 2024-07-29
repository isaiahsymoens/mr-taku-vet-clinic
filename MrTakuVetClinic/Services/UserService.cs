using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }
            return user;
        }

        public async Task<IEnumerable<User>> GetSearchUsersAsync([FromQuery] string firstName, string lastName)
        {
            return await _userRepository.GetSearchUsersAsync(firstName, lastName);
        }

        public async Task AddUserAsync(User user)
        {
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
