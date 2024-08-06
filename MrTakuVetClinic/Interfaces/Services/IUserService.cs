using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.DTOs.User;
using MrTakuVetClinic.Models;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Services
{
    public interface IUserService
    {
        Task<ApiResponse<UserDto>> GetAllUsersAsync();
        Task<ApiResponse<UserDto>> GetUserByUsernameAsync(string username);
        Task<ApiResponse<UserDto>> GetSearchUsersAsync([FromQuery] string firstName, [FromQuery] string lastName);
        Task<ApiResponse<UserDto>> PostUserAsync(UserPostDto userPostDto);
        Task<ApiResponse<UserDto>> UpdateUserAsync(UserUpdateDto userUpdateDto);
        Task<ApiResponse<UserDto>> DeleteUserByUsernameAsync(string username);
    }
}
