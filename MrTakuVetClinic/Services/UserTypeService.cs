using AutoMapper;
using FluentValidation;
using MrTakuVetClinic.DTOs.UserType;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Helpers;
using MrTakuVetClinic.Interfaces.Repositories;
using MrTakuVetClinic.Interfaces.Services;
using MrTakuVetClinic.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Services
{
    public class UserTypeService : IUserTypeService
    {
        private readonly IUserTypeRepository _userTypeRepository;
        private readonly IValidator<UserTypePostDto> _userTypevalidator;
        private readonly IMapper _mapper;

        public UserTypeService(
            IUserTypeRepository userTypeRepository, 
            IValidator<UserTypePostDto> userTypeValidator,
            IMapper mapper)
        {
            _userTypeRepository = userTypeRepository;
            _userTypevalidator = userTypeValidator;
            _mapper = mapper;
        }

        public async Task<ApiResponse<UserTypeDto>> GetAllUserTypesAsync()
        {
            return ApiResponseHelper
                .SuccessResponse<UserTypeDto>(
                200,
                (await _userTypeRepository.GetAllAsync())
                .Select(u => _mapper.Map<UserTypeDto>(u))
            );
        }

        public async Task<ApiResponse<UserTypeDto>> GetUserTypeByIdAsync(int id)
        {
            var userType = await _userTypeRepository.GetByIdAsync(id);
            if (userType == null)
            {
                return ApiResponseHelper.FailResponse<UserTypeDto>(
                    404, 
                    new { Message = "User type does not exist." }
                );

            }
            return ApiResponseHelper.SuccessResponse<UserTypeDto>(
                200, 
                _mapper.Map<UserTypeDto>(userType)
            );
        }

        public async Task<ApiResponse<UserTypeDto>> PostUserTypeAsync(UserType userType)
        {
            if (await _userTypeRepository.IsTypeNameExits(userType.TypeName))
            {
                return ApiResponseHelper.FailResponse<UserTypeDto>(400, new { TypeName = "Type name already exists." });
            }
            await _userTypeRepository.AddAsync(userType);
            return ApiResponseHelper.SuccessResponse<UserTypeDto>(204, null);
        }

        public async Task<ApiResponse<UserTypeDto>> DeleteUserTypeAsync(int id)
        {
            if (await _userTypeRepository.GetByIdAsync(id) == null)
            {
                return ApiResponseHelper.FailResponse<UserTypeDto>(404, new { Message = "User type not found." });
            }
            await _userTypeRepository.DeleteAsync(id);
            return ApiResponseHelper.SuccessResponse<UserTypeDto>(204, null);
        }
    }
}
