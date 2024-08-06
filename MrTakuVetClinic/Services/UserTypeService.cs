using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Helpers;
using MrTakuVetClinic.Interfaces;
using MrTakuVetClinic.Interfaces.Services;
using MrTakuVetClinic.Models;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Services
{
    public class UserTypeService : IUserTypeService
    {
        private readonly IUserTypeRepository _userTypeRepository;

        public UserTypeService(IUserTypeRepository userTypeRepository)
        {
            _userTypeRepository = userTypeRepository;
        }

        public async Task<ApiResponse<UserType>> GetAllUserTypesAsync()
        { 
            return ApiResponseHelper
                .SuccessResponse<UserType>(200, await _userTypeRepository.GetAllAsync());
        }

        public async Task<ApiResponse<UserType>> GetUserTypeByIdAsync(int id)
        {
            var userType = await _userTypeRepository.GetByIdAsync(id);
            if (userType == null)
            {
                return ApiResponseHelper.FailResponse<UserType>(404, new { Message = "User type does not exist." });

            }
            return ApiResponseHelper
                .SuccessResponse<UserType>(200, userType);
        }

        public async Task<ApiResponse<UserType>> PostUserTypeAsync(UserType userType)
        {
            await _userTypeRepository.AddAsync(userType);
            return ApiResponseHelper
                .SuccessResponse<UserType>(204, null);
        }
    }
}
