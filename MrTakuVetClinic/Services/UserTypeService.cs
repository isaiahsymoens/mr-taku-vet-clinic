using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Services
{
    public class UserTypeService
    {
        private readonly IUserTypeRepository _userTypeRepository;

        public UserTypeService(IUserTypeRepository userTypeRepository)
        {
            _userTypeRepository = userTypeRepository;
        }

        public async Task<IEnumerable<UserType>> GetAllUserTypesAsync()
        { 
            return await _userTypeRepository.GetAllAsync();
        }

        public async Task<UserType> GetUserTypeByIdAsync(int id)
        {
            return await _userTypeRepository.GetByIdAsync(id);
        }

        public async Task PostUserTypeAsync(UserType userType)
        {
            await _userTypeRepository.AddAsync(userType);
        }
    }
}
