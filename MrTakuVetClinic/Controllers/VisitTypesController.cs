using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Data;
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
            return Ok(await _visitTypeService.GetAllVisitTypesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVisitTypeById(int id)
        {
            var visitType = await _visitTypeService.GetVisitTypeByIdAsync(id);
            if (visitType == null)
            {
                return NotFound();
            }

            return Ok(visitType);
        }

        [HttpPost]
        public async Task<IActionResult> PostVisitTypeAsync(VisitType visitType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _visitTypeService.PostVisitTypeAsync(visitType);
            return CreatedAtAction(nameof(GetVisitTypeById), new { id = visitType.VisitTypeId }, visitType);
        }
    }
}
