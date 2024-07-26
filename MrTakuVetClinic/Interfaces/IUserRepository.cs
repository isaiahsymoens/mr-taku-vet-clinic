using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>> GetSearchUsersAsync([FromQuery] string firstName, string lastName); 
        Task DeleteUserByUsernameAsync(string username);
    }
}
