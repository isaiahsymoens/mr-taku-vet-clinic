using MrTakuVetClinic.DTOs.UserType;
using MrTakuVetClinic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Services
{
    public interface IUserTypeService
    {
        Task<ApiResponse<IEnumerable<UserTypeDto>>> GetAllUserTypesAsync();
        Task<ApiResponse<UserTypeDto>> GetUserTypeByIdAsync(int id);
        Task<ApiResponse<UserTypeDto>> PostUserTypeAsync(UserTypePostDto userTypeUpdateDto);
        Task<ApiResponse<UserTypeDto>> UpdateUserTypeAsync(int id, UserTypeUpdateDto userTypeUpdateDto);
        Task<ApiResponse<UserTypeDto>> DeleteUserTypeAsync(int id);
    }
}
