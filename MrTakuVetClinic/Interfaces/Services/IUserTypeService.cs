using MrTakuVetClinic.DTOs.UserType;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Models;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Services
{
    public interface IUserTypeService
    {
        Task<ApiResponse<UserTypeDto>> GetAllUserTypesAsync();
        Task<ApiResponse<UserTypeDto>> GetUserTypeByIdAsync(int id);
        Task<ApiResponse<UserTypeDto>> PostUserTypeAsync(UserType userType);
        Task<ApiResponse<UserTypeDto>> DeleteUserTypeAsync(int id);
    }
}
