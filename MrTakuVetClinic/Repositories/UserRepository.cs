using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.DTOs.User;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Pets)
                .Include(u => u.UserType)
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .ToListAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<IEnumerable<User>> GetSearchUsersAsync(UserSearchDto userSearchDto)
        {
            var query = _context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(userSearchDto.FirstName) || !string.IsNullOrEmpty(userSearchDto.LastName))
            {
                query = query.Where(u =>
                    (string.IsNullOrEmpty(userSearchDto.FirstName) || u.FirstName.ToLower().Contains(userSearchDto.FirstName.ToLower())) &&
                    (string.IsNullOrEmpty(userSearchDto.LastName) || u.LastName.ToLower().Contains(userSearchDto.LastName.ToLower()))
                );
            }
            return await query
                .Include(u => u.UserType)
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .ToListAsync();
        }

        public async Task DeleteUserByUsernameAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsEmailExits(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> IsUsernameExits(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }
    }
}
