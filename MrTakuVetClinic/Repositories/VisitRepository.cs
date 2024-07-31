using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.DTOs.Visit;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces;
using System.Collections.Generic;
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
                .Include(v => v.Pet)
                .ThenInclude(v => v.User)
                .ThenInclude(v => v.UserType)
                .ToListAsync();
        }

        public async Task<IEnumerable<Visit>> SearchVisitsAsync(VisitFilterDto visitFilterDto)
        {
            //var query = _context.Users.AsQueryable();
            //if (!string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName))
            //{
            //    query = query.Where(u =>
            //        (string.IsNullOrEmpty(firstName) || u.FirstName.Contains(firstName)) &&
            //        (string.IsNullOrEmpty(lastName) || u.LastName.Contains(lastName))
            //    );
            //}

            return await _context.Visits
                //.Include(v => v.Pet)
                .ToListAsync();
        }

        public async Task<Visit> GetVisitByIdAsync(int id)
        {
            return await _context.Visits
                //.Include(v => v.Pet)
                //.ThenInclude(v => v.User)
                //.ThenInclude(v => v.UserType)
                .FirstOrDefaultAsync(v => v.VisitId == id);
        }
    }
}
