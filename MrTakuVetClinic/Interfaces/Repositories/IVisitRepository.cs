﻿using MrTakuVetClinic.DTOs.Visit;
using MrTakuVetClinic.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Repositories
{
    public interface IVisitRepository : IRepository<Visit>
    {
        Task<IEnumerable<Visit>> GetAllVisitsAsync();
        Task<Visit> GetVisitByIdAsync(int id);
        Task<IEnumerable<Visit>> SearchVisitsAsync(VisitSearchDto visitSearchDto);
    }
}
