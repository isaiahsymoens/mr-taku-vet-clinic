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

        public async Task<IEnumerable<Visit>> GetPetVisitsByIdAsync(int id)
        {
            return await _context.Visits
                .Where(v => v.PetId == id)
                .Include(v => v.VisitType)
                .Include(v => v.Pet)
                .ThenInclude(v => v.PetType)
                .Include(v => v.Pet.User)
                .ThenInclude(v => v.UserType)
                .ToListAsync();
        }

        public async Task<IEnumerable<Visit>> SearchVisitsAsync(VisitSearchDto visitSearchDto)
        {
            var visits = await _context.Visits
                .Include(v => v.VisitType)
                .Include(v => v.Pet)
                .ThenInclude(v => v.PetType)
                .Include(v => v.Pet.User)
                .ThenInclude(v => v.UserType)
                .ToListAsync();

            return (visits.AsQueryable()).Where(v =>
                (string.IsNullOrEmpty(visitSearchDto.FirstName) || v.Pet.User.FirstName.ToLower().Contains(visitSearchDto.FirstName.ToLower())) &&
                (string.IsNullOrEmpty(visitSearchDto.LastName) || v.Pet.User.LastName.ToLower().Contains(visitSearchDto.LastName.ToLower())) &&
                (string.IsNullOrEmpty(visitSearchDto.PetName) || v.Pet.PetName.ToLower().Contains(visitSearchDto.PetName.ToLower())) &&
                (string.IsNullOrEmpty(visitSearchDto.PetType) || v.Pet.PetType.TypeName.ToLower().Contains(visitSearchDto.PetType.ToLower())) &&
                (string.IsNullOrEmpty(visitSearchDto.VisitType) || v.VisitType.TypeName.ToLower().Contains(visitSearchDto.VisitType.ToLower())) &&
                (!visitSearchDto.VisitDateFrom.HasValue || v.Date >= visitSearchDto.VisitDateFrom.Value) &&
                (!visitSearchDto.VisitDateTo.HasValue || v.Date <= visitSearchDto.VisitDateTo.Value)
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
