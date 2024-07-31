using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.DTOs.Pet;
using MrTakuVetClinic.DTOs.User;
using MrTakuVetClinic.DTOs.Visit;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                .ThenInclude(v => v.User)
                .ThenInclude(v => v.UserType)
                .ToListAsync();
        }

        public async Task<IEnumerable<Visit>> SearchVisitsAsync(VisitFilterDto visitFilterDto)
        {
            var test = await _context.Visits
                .Include(v => v.VisitType)
                .Include(v => v.Pet)
                .ThenInclude(v => v.User)
                .ThenInclude(v => v.UserType)
                .ToListAsync();

            //return visits.Select(v => new VisitDto
            //{
            //    VisitId = v.VisitId,
            //    Date = v.Date,
            //    PetId = v.PetId,
            //    Pet = new PetDto
            //    {
            //        PetId = v.Pet.PetId,
            //        PetName = v.Pet.PetName,
            //        PetTypeId = v.Pet.PetTypeId,
            //        Breed = v.Pet.Breed,
            //        BirthDate = v.Pet.BirthDate,
            //        User = new UserDto
            //        {
            //            FirstName = v.Pet.User.FirstName,
            //            MiddleName = v.Pet.User.MiddleName,
            //            LastName = v.Pet.User.LastName,
            //            Email = v.Pet.User.Email,
            //            Username = v.Pet.User.Username,
            //            Active = v.Pet.User.Active,
            //            UserType = v.Pet.User.UserType.TypeName
            //        }
            //    }
            //}).ToList();

            var query = test.AsQueryable();
            query = query.Where(v =>
                (string.IsNullOrEmpty(visitFilterDto.FirstName) || v.Pet.User.FirstName.ToLower().Contains(visitFilterDto.FirstName.ToLower())) ||
                (string.IsNullOrEmpty(visitFilterDto.LastName) || v.Pet.User.LastName.ToLower().Contains(visitFilterDto.LastName.ToLower())) ||

                (string.IsNullOrEmpty(visitFilterDto.PetName) || v.Pet.PetName.ToLower().Contains(visitFilterDto.PetName.ToLower())) ||
            //(string.IsNullOrEmpty(visitFilterDto.PetType) || v.Pet.PetType.TypeName.ToLower().Contains(visitFilterDto.PetType.ToLower()))
                (string.IsNullOrEmpty(visitFilterDto.VisitType) || v.VisitType.TypeName.ToLower().Contains(visitFilterDto.VisitType.ToLower()))


            //(string.IsNullOrEmpty(visitFilterDto.LastName) || v.Pet.User.LastName.ToLower().Contains(visitFilterDto.LastName.ToLower())) ||
            );
            Console.WriteLine("###########################################################################################");
            //Console.WriteLine("test2 :", query);

            return query;
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
