using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.DTOs.Visit;
using MrTakuVetClinic.Models;
using MrTakuVetClinic.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitsController : Controller
    {
        private readonly VisitService _visitService;
        public VisitsController(VisitService visitService)
        {
            _visitService = visitService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitDto>>> GetAllVisitsAsync()
        {
            var response = await _visitService.GetAllVisitsAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("paginated")]
        public async Task<ActionResult<IEnumerable<VisitDto>>> GetAllPaginatedVisitsAsync([FromQuery] PaginationParameters paginationParams)
        {
            var response = await _visitService.GetAllPaginatedVisitsAsync(paginationParams);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVisitByIdAsync(int id)
        {
            var response = await _visitService.GetVisitById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("petvisits/{id}")]
        public async Task<IActionResult> GetPetVisitsByIdAsync(int id)
        {
            var response = await _visitService.GetPetVisitsByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchVisitsAsync([FromBody] VisitSearchDto visitSearchDto, [FromQuery] PaginationParameters paginationParams)
        {
            var response = await _visitService.SearchVisitsAsync(visitSearchDto, paginationParams);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<VisitDto>> PostVisit(VisitPostDto visitPostDto)
        {
            var response = await _visitService.PostVisitAsync(visitPostDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVisitRecordById(int id, [FromBody] VisitUpdateDto visitUpdateDto)
        {
            var response = await _visitService.UpdatePetByIdAsync(id, visitUpdateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitRecordAsync(int id)
        {
            var response = await _visitService.DeleteVisitAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
