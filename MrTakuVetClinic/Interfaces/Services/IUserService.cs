using MrTakuVetClinic.DTOs.User;
using MrTakuVetClinic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Services
{
    public interface IUserService
    {
        Task<ApiResponse<PaginatedResponse<UserDto>>> GetAllPaginatedUsersAsync(PaginationParameters paginationParameters);
        Task<ApiResponse<IEnumerable<UserDto>>> GetAllUsersAsync();
        Task<ApiResponse<UserDto>> GetUserByUsernameAsync(string username);
        Task<ApiResponse<PaginatedResponse<UserDto>>> GetSearchUsersAsync(UserSearchDto userSearchDto);
        Task<ApiResponse<UserPassword>> GetUserPasswordByUsernameAsync(string username);
        Task<ApiResponse<UserDto>> PostUserAsync(UserPostDto userPostDto);
        Task<ApiResponse<UserDto>> UpdateUserAsync(String username, UserUpdateDto userUpdateDto);
        Task<ApiResponse<UserDto>> DeleteUserByUsernameAsync(string username);
    }
}
