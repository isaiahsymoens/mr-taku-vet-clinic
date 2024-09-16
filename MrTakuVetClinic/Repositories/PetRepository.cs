using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces.Repositories;
using MrTakuVetClinic.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Repositories
{
    public class PetRepository : Repository<Pet>, IPetRepository
    {
        public PetRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Pet>> GetAllPetsAsync()
        {
            return await _context.Pets
                .Include(p => p.Visits)
                .Include(p => p.PetType)
                .Include(p => p.User)
                .ThenInclude(p => p.UserType)
                .OrderBy(p => p.PetName)
                .ToListAsync();
        }

        public async Task<PaginatedResponse<Pet>> GetPaginatedPetsAsync(PaginationParameters paginationParams)
        {
            var totalItems = await _context.Pets.CountAsync();
            var pets = await _context.Pets
                .Include(p => p.Visits)
                .Include(p => p.PetType)
                .Include(p => p.User)
                .ThenInclude(p => p.UserType)
                .OrderBy(p => p.PetName)
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();
            return new PaginatedResponse<Pet>(pets, paginationParams.PageNumber, paginationParams.PageSize, totalItems);
        }

        public async Task<PaginatedResponse<Pet>> GetAllUserPetsAsync(string username, PaginationParameters paginationParams)
        {
            var query = _context.Pets
                .Where(p => p.User.Username == username)
                .Include(p => p.Visits)
                .Include(p => p.PetType)
                .Include(p => p.User)
                .ThenInclude(p => p.UserType)
                .OrderBy(p => p.PetName)
                .AsQueryable();

            var totalItems = await query.CountAsync();
            var pets = await query
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            return new PaginatedResponse<Pet>(pets, paginationParams.PageNumber, paginationParams.PageSize, totalItems);
        }

        public async Task<Pet> GetPetByIdAsync(int id)
        {
            return await _context.Pets
                .Include(p => p.PetType)
                .Include(p => p.User)
                .ThenInclude(p => p.UserType)
                .FirstOrDefaultAsync(p => p.PetId == id);
        }
    }
}
