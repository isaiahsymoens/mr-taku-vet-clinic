using MrTakuVetClinic.DTOs.User;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<PaginatedResponse<User>> GetPaginatedUsersAsync(PaginationParameters paginationParams, UserSortDto userSortDto);
        Task<User> GetUserByUsernameAsync(string username);
        Task<PaginatedResponse<User>> GetSearchUsersAsync(UserSearchDto userSearchDto, UserSortDto userSortDto);
        Task DeleteUserByUsernameAsync(string username);
        Task<bool> IsEmailExits(string email);
        Task<bool> IsUsernameExits(string username);
    }
}
