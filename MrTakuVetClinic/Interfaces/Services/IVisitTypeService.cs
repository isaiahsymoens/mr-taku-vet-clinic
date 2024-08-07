using MrTakuVetClinic.DTOs.VisitType;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Models;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Services
{
    public interface IVisitTypeService
    {
        Task<ApiResponse<VisitTypeDto>> GetAllVisitTypesAsync();
        Task<ApiResponse<VisitTypeDto>> GetVisitTypeByIdAsync(int id);
        Task<ApiResponse<VisitType>> PostVisitTypeAsync(VisitType visitType);
    }
}
