using MrTakuVetClinic.DTOs.VisitType;
using MrTakuVetClinic.Models;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Services
{
    public interface IVisitTypeService
    {
        Task<ApiResponse<VisitTypeDto>> GetAllVisitTypesAsync();
        Task<ApiResponse<VisitTypeDto>> GetVisitTypeByIdAsync(int id);
        Task<ApiResponse<VisitTypeDto>> PostVisitTypeAsync(VisitTypePostDto visitTypePostDto);
        Task<ApiResponse<VisitTypeDto>> UpdateVisitTypeAsync(int id, VisitTypeUpdateDto visitTypeUpdateDto);
        Task<ApiResponse<VisitTypeDto>> DeleteVisitTypeAsync(int id);
    }
}
