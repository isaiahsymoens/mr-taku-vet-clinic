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
        Task<ApiResponse<PaginatedResponse<VisitDto>>> GetAllPaginatedVisitsAsync(PaginationParameters paginationParams);
        Task<ApiResponse<VisitDto>> GetVisitById(int id);
        Task<ApiResponse<PaginatedResponse<VisitDto>>> GetPetVisitsByIdAsync(int id, PaginationParameters paginationParams);
        Task<ApiResponse<PaginatedResponse<VisitDto>>> SearchVisitsAsync(VisitSearchDto visitSearchDto, PaginationParameters paginationParams);
        Task<ApiResponse<VisitDto>> PostVisitAsync(VisitPostDto visitPostDto);
        Task<ApiResponse<VisitDto>> UpdatePetByIdAsync(int id, VisitUpdateDto visitUpdateDto);
        Task<ApiResponse<VisitDto>> DeleteVisitAsync(int id);
    }
}
