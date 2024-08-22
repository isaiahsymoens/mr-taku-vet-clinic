using MrTakuVetClinic.DTOs.User;
using MrTakuVetClinic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Services
{
    public interface IUserService
    {
        Task<ApiResponse<IEnumerable<UserDto>>> GetAllUsersAsync();
        Task<ApiResponse<UserDto>> GetUserByUsernameAsync(string username);
        Task<ApiResponse<IEnumerable<UserDto>>> GetSearchUsersAsync(UserSearchDto userSearchDto);
        Task<ApiResponse<UserDto>> PostUserAsync(UserPostDto userPostDto);
        Task<ApiResponse<UserDto>> UpdateUserAsync(UserUpdateDto userUpdateDto);
        Task<ApiResponse<UserDto>> DeleteUserByUsernameAsync(string username);
    }
}
