using MrTakuVetClinic.DTOs.UserType;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Models;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Services
{
    public interface IUserTypeService
    {
        Task<ApiResponse<UserTypeDto>> GetAllUserTypesAsync();
        Task<ApiResponse<UserType>> GetUserTypeByIdAsync(int id);
        Task<ApiResponse<UserType>> PostUserTypeAsync(UserType userType);
    }
}
