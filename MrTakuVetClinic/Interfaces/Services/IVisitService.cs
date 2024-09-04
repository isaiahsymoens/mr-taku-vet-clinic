using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.DTOs.Visit;
using MrTakuVetClinic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Services
{
    public interface IVisitService
    {
        Task<ApiResponse<IEnumerable<VisitDto>>> GetAllVisitsAsync();
        Task<ApiResponse<VisitDto>> GetVisitById(int id);
        Task<ApiResponse<IEnumerable<VisitDto>>> GetPetVisitsByIdAsync(int id);
        Task<ApiResponse<IEnumerable<VisitDto>>> SearchVisitsAsync([FromQuery] VisitSearchDto visitSearchDto);
        Task<ApiResponse<VisitDto>> PostVisitAsync(VisitPostDto visitPostDto);
        Task<ApiResponse<VisitDto>> DeleteVisitAsync(int id);
    }
}
