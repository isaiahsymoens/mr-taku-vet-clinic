using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.DTOs.VisitType;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Services;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitTypesController : Controller
    {
        private VisitTypeService _visitTypeService;

        public VisitTypesController(VisitTypeService visitTypeService)
        {
            _visitTypeService = visitTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVisitTypesAsync()
        {
            var response = await _visitTypeService.GetAllVisitTypesAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVisitTypeById(int id)
        {
            var response = await _visitTypeService.GetVisitTypeByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> PostVisitTypeAsync(VisitType visitType)
        {
            var response = await _visitTypeService.PostVisitTypeAsync(visitType);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisitTypeAsync(int id, [FromBody] VisitTypeUpdateDto vistTypeUpdateDto)
        {
            var response = await _visitTypeService.UpdateVisitTypeAsync(id, vistTypeUpdateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVisitTypeAsync(int id)
        {
            var response = await _visitTypeService.DeleteVisitTypeAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
