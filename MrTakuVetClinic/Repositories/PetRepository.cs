using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.DTOs.Pet;
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

        public async Task<PaginatedResponse<Pet>> GetPaginatedPetsAsync(PaginationParameters paginationParams, PetSortDto petSortDto)
        {
            var totalItems = await _context.Pets.CountAsync();
            var query = _context.Pets
                .Include(p => p.Visits)
                .Include(p => p.PetType)
                .Include(p => p.User)
                .ThenInclude(p => p.UserType)
                .OrderBy(p => p.PetName)
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize);

            query = ApplyOrderBy(query, petSortDto.SortBy, petSortDto.Ascending);

            return new PaginatedResponse<Pet>(await query.ToListAsync(), paginationParams.PageNumber, paginationParams.PageSize, totalItems);
        }

        public async Task<IEnumerable<Pet>> GetAllUserPetsAsync(string username)
        {
            return await _context.Pets
                .Where(p => p.User.Username == username)
                .Include(p => p.Visits)
                .Include(p => p.PetType)
                .Include(p => p.User)
                .ThenInclude(p => p.UserType)
                .OrderBy(p => p.PetName)
                .ToListAsync();
        }

        public async Task<PaginatedResponse<Pet>> GetAllPaginatedUserPetsAsync(string username, PaginationParameters paginationParams)
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

        private IQueryable<Pet> ApplyOrderBy(IQueryable<Pet> query, string sortBy, bool ascending)
        {
            switch (sortBy)
            {
                case "PetName":
                    return ascending ? query.OrderBy(p => p.PetName) : query.OrderByDescending(p => p.PetName);
                case "BirthDate":
                    return ascending ? query.OrderBy(p => p.BirthDate) : query.OrderByDescending(p => p.BirthDate);
                case "NoOfVisits":
                    return ascending ? query.OrderBy(p => p.PetName) : query.OrderByDescending(p => p.PetName);
                default:
                    return ascending ? query.OrderBy(p => p.PetName) : query.OrderByDescending(p => p.PetName);
            }
        }
    }
}
