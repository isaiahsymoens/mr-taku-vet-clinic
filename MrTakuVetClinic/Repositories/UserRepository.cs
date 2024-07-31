using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces;
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
                .Include(u => u.UserType)
                .ToListAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<IEnumerable<User>> GetSearchUsersAsync(string firstName, string lastName)
        {
            var query = _context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName))
            {
                query = query.Where(u =>
                    (string.IsNullOrEmpty(firstName) || u.FirstName.ToLower().Contains(firstName.ToLower())) &&
                    (string.IsNullOrEmpty(lastName) || u.LastName.ToLower().Contains(lastName.ToLower()))
                );
            }
            return await query
                .Include(u => u.UserType)
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
