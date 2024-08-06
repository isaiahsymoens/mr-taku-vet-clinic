using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Helpers;
using MrTakuVetClinic.Interfaces.Repositories;
using MrTakuVetClinic.Interfaces.Services;
using MrTakuVetClinic.Models;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Services
{
    public class VisitTypeService : IVisitTytpeService
    {
        private readonly IVisitTypeRepository _visitTypeRepository;

        public VisitTypeService(IVisitTypeRepository visitTypeRepository)
        {
            _visitTypeRepository = visitTypeRepository;
        }

        public async Task<ApiResponse<VisitType>> GetAllVisitTypesAsync()
        {
            return ApiResponseHelper
                .SuccessResponse<VisitType>(200, await _visitTypeRepository.GetAllAsync());
        }

        public async Task<ApiResponse<VisitType>> GetVisitTypeByIdAsync(int id)
        {
            var visitType = await _visitTypeRepository.GetByIdAsync(id);
            if (visitType == null)
            {
                return ApiResponseHelper.FailResponse<VisitType>(404, new { Message = "Visit type does not exist." });
            }
            return ApiResponseHelper
                .SuccessResponse<VisitType>(200, visitType);
        }

        public async Task<ApiResponse<VisitType>> PostVisitTypeAsync(VisitType visitType)
        {
            await _visitTypeRepository.AddAsync(visitType);
            return ApiResponseHelper
                .SuccessResponse<VisitType>(204, null);
        }
    }
}
