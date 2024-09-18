using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.DTOs.User;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces.Repositories;
using MrTakuVetClinic.Models;
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

        public async Task<PaginatedResponse<User>> GetPaginatedUsersAsync(PaginationParameters paginationParams, UserSortDto userSortDto)
        {
            var totalItems = await _context.Users.Where(u => u.UserId != 1).CountAsync();
            var users = _context.Users
                .Include(u => u.Pets)
                .Include(u => u.UserType)
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .Where(u => u.UserTypeId != 1)
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize);

            users = ApplyOrderBy(users, userSortDto.SortBy, userSortDto.Ascending);

            return new PaginatedResponse<User>(await users.ToListAsync(), paginationParams.PageNumber, paginationParams.PageSize, totalItems);
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

        public async Task<PaginatedResponse<User>> GetSearchUsersAsync(UserSearchDto userSearchDto, UserSortDto userSortDto)
        {
            var paginationParams = new PaginationParameters();
            var query = _context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(userSearchDto.Name))
            {
                var name = userSearchDto.Name.Trim().ToLower();
                query = query.Where(u =>
                    (u.FirstName + " " + (u.MiddleName ?? "") + u.LastName).ToLower().Contains(name)
                );
            }
            var totalItems = await query.Where(u => u.UserTypeId != 1).CountAsync();
            var users = query
                .Include(u => u.UserType)
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .Where(u => u.UserTypeId != 1)
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize);

            users = ApplyOrderBy(users, userSortDto.SortBy, userSortDto.Ascending);

            return new PaginatedResponse<User>(await users.ToListAsync(), paginationParams.PageNumber, paginationParams.PageSize, totalItems);
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

        private IQueryable<User> ApplyOrderBy(IQueryable<User> query, string sortBy, bool ascending)
        {
            switch (sortBy.ToLower())
            {
                case "name":
                    return ascending ? query.OrderBy(u => u.FirstName).ThenBy(u => u.LastName) : query.OrderByDescending(u => u.FirstName).ThenBy(u => u.LastName);
                case "firstname":
                    return ascending ? query.OrderBy(u => u.FirstName).ThenBy(u => u.LastName) : query.OrderByDescending(u => u.FirstName).ThenBy(u => u.LastName);
                case "lastname":
                    return ascending ? query.OrderBy(u => u.LastName).ThenBy(u => u.FirstName) : query.OrderByDescending(u => u.LastName).ThenBy(u => u.FirstName);
                case "email":
                    return ascending ? query.OrderBy(u => u.Email) : query.OrderByDescending(u => u.Email);
                case "petowned":
                    return ascending ? query.OrderBy(u => u.Pets.Count) : query.OrderByDescending(u => u.Pets.Count);
                case "active":
                    return ascending ? query.OrderBy(u => u.Active) : query.OrderByDescending(u => u.Active);
                default:
                    return ascending ? query.OrderBy(u => u.FirstName).ThenBy(u => u.LastName) : query.OrderByDescending(u => u.FirstName).ThenBy(u => u.LastName);
            }
        }
    }
}
