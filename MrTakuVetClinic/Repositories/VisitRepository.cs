using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.DTOs.Visit;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces.Repositories;
using MrTakuVetClinic.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Repositories
{
    public class VisitRepository : Repository<Visit>, IVisitRepository
    {
        public VisitRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Visit>> GetAllVisitsAsync()
        {
            return await _context.Visits
                .Include(v => v.VisitType)
                .Include(v => v.Pet)
                .ThenInclude(v => v.PetType)
                .Include(v => v.Pet.User)
                .ThenInclude(v => v.UserType)
                .ToListAsync();
        }

        public async Task<PaginatedResponse<Visit>> GetAllPaginatedVisitsAsync(PaginationParameters paginationParams, VisitSortDto visitSortDto)
        {
            var totalItems = await _context.Visits.CountAsync();
            var visits = _context.Visits
                .Include(v => v.VisitType)
                .Include(v => v.Pet)
                .ThenInclude(v => v.PetType)
                .Include(v => v.Pet.User)
                .ThenInclude(v => v.UserType)
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize);

            visits = ApplyOrderBy(visits, visitSortDto.SortBy, visitSortDto.Ascending);

            return new PaginatedResponse<Visit>(await visits.ToListAsync(), paginationParams.PageNumber, paginationParams.PageSize, totalItems);
        }

        public async Task<PaginatedResponse<Visit>> GetPetVisitsByIdAsync(int id, PaginationParameters paginationParams, VisitSortDto visitSortDto)
        {
            var query = _context.Visits
                .Where(v => v.PetId == id)
                .Include(v => v.VisitType)
                .Include(v => v.Pet)
                .ThenInclude(v => v.PetType)
                .Include(v => v.Pet.User)
                .ThenInclude(v => v.UserType)
                .AsQueryable();

            var totalItems = await query.CountAsync();

            query = ApplyOrderBy(query, visitSortDto.SortBy, visitSortDto.Ascending);

            var visits = await query
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            return new PaginatedResponse<Visit>(visits, paginationParams.PageNumber, paginationParams.PageSize, totalItems);
        }

        public async Task<PaginatedResponse<Visit>> SearchVisitsAsync(VisitSearchDto visitSearchDto, PaginationParameters paginationParams, VisitSortDto visitSortDto)
        {
            var query = _context.Visits
                .Include(v => v.VisitType)
                .Include(v => v.Pet)
                .ThenInclude(v => v.PetType)
                .Include(v => v.Pet.User)
                .ThenInclude(v => v.UserType)
                .AsQueryable();

            query = (query.AsQueryable()).Where(v =>
                (string.IsNullOrEmpty(visitSearchDto.FirstName) || v.Pet.User.FirstName.ToLower().Contains(visitSearchDto.FirstName.ToLower())) &&
                (string.IsNullOrEmpty(visitSearchDto.LastName) || v.Pet.User.LastName.ToLower().Contains(visitSearchDto.LastName.ToLower())) &&
                (string.IsNullOrEmpty(visitSearchDto.PetName) || v.Pet.PetName.ToLower().Contains(visitSearchDto.PetName.ToLower())) &&
                (string.IsNullOrEmpty(visitSearchDto.PetType) || v.Pet.PetType.TypeName.ToLower().Contains(visitSearchDto.PetType.ToLower())) &&
                (string.IsNullOrEmpty(visitSearchDto.VisitType) || v.VisitType.TypeName.ToLower().Contains(visitSearchDto.VisitType.ToLower())) &&
                (!visitSearchDto.VisitDateFrom.HasValue || v.Date.Date >= visitSearchDto.VisitDateFrom.Value.Date) &&
                (!visitSearchDto.VisitDateTo.HasValue || v.Date.Date <= visitSearchDto.VisitDateTo.Value.Date)
            );

            var totalItems = await query.CountAsync();
            if (paginationParams.PageSize > 0)
            {
                query = query
                    .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                    .Take(paginationParams.PageSize);
            }
            var visits = ApplyOrderBy(query, visitSortDto.SortBy, visitSortDto.Ascending);

            return new PaginatedResponse<Visit>(await visits.ToListAsync(), paginationParams.PageNumber, paginationParams.PageSize, totalItems);
        }

        public async Task<Visit> GetVisitByIdAsync(int id)
        {
            return await _context.Visits
                .Include(v => v.VisitType)
                .Include(v => v.Pet)
                .ThenInclude(v => v.PetType)
                .Include(v => v.Pet.User)
                .ThenInclude(v => v.UserType)
                .FirstOrDefaultAsync(v => v.VisitId == id);
        }

        private IQueryable<Visit> ApplyOrderBy(IQueryable<Visit> query, string sortBy, bool ascending)
        {
            switch (sortBy.ToLower())
            {
                case "owner":
                case "pet.user.name":
                    return ascending ? query.OrderBy(v => v.Pet.User.FirstName).ThenBy(v => v.Pet.User.LastName) : query.OrderByDescending(v => v.Pet.User.FirstName).ThenBy(v => v.Pet.User.LastName);
                case "Pet":
                case "pet.petname":
                    return ascending ? query.OrderBy(v => v.Pet.PetName) : query.OrderByDescending(v => v.Pet.PetName);
                case "VisitType":
                case "visittype.typename":
                    return ascending ? query.OrderBy(v => v.VisitType.TypeName) : query.OrderByDescending(v => v.VisitType.TypeName);
                case "Visitdate":
                case "date":
                    return ascending ? query.OrderBy(v => v.Date) : query.OrderByDescending(v => v.Date);
                default:
                    return ascending ? query.OrderBy(v => v.Pet.User.FirstName).ThenBy(v => v.Pet.User.LastName) : query.OrderByDescending(v => v.Pet.User.FirstName).ThenBy(v => v.Pet.User.LastName);
            }
        }
    }
}
