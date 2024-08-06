using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Models;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Services
{
    public interface IVisitTytpeService
    {
        Task<ApiResponse<VisitType>> GetAllVisitTypesAsync();
        Task<ApiResponse<VisitType>> GetVisitTypeByIdAsync(int id);
        Task<ApiResponse<VisitType>> PostVisitTypeAsync(VisitType visitType);
    }
}
