using MrTakuVetClinic.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>> GetSearchUsersAsync(string firstName, string lastName);
        Task DeleteUserByUsernameAsync(string username);
        Task<bool> IsEmailExits(string email);
        Task<bool> IsUsernameExits(string username);
    }
}
