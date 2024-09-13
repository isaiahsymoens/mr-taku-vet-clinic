using MrTakuVetClinic.DTOs.User;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<PaginatedResponse<User>> GetPaginatedUsersAsync(PaginationParameters paginationParams);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>> GetSearchUsersAsync(UserSearchDto userSearchDto);
        Task DeleteUserByUsernameAsync(string username);
        Task<bool> IsEmailExits(string email);
        Task<bool> IsUsernameExits(string username);
    }
}
