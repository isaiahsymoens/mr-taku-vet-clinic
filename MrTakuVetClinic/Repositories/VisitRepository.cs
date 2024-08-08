using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.DTOs.Visit;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces.Repositories;
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

        public async Task<IEnumerable<Visit>> SearchVisitsAsync(VisitFilterDto visitFilterDto)
        {
            var visits = await _context.Visits
                .Include(v => v.VisitType)
                .Include(v => v.Pet)
                .ThenInclude(v => v.PetType)
                .Include(v => v.Pet.User)
                .ThenInclude(v => v.UserType)
                .ToListAsync();

            return (visits.AsQueryable()).Where(v =>
                (string.IsNullOrEmpty(visitFilterDto.FirstName) || v.Pet.User.FirstName.ToLower().Contains(visitFilterDto.FirstName.ToLower())) &&
                (string.IsNullOrEmpty(visitFilterDto.LastName) || v.Pet.User.LastName.ToLower().Contains(visitFilterDto.LastName.ToLower())) &&
                (string.IsNullOrEmpty(visitFilterDto.PetName) || v.Pet.PetName.ToLower().Contains(visitFilterDto.PetName.ToLower())) &&
                (string.IsNullOrEmpty(visitFilterDto.PetType) || v.Pet.PetType.TypeName.ToLower().Contains(visitFilterDto.PetType.ToLower())) &&
                (string.IsNullOrEmpty(visitFilterDto.VisitType) || v.VisitType.TypeName.ToLower().Contains(visitFilterDto.VisitType.ToLower())) &&
                (!visitFilterDto.VisitDateFrom.HasValue || v.Date >= visitFilterDto.VisitDateFrom.Value) &&
                (!visitFilterDto.VisitDateTo.HasValue || v.Date <= visitFilterDto.VisitDateTo.Value)
            );
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
    }
}
