using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.DTOs.Visit;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitsController : Controller
    {
        // TODO:
        // Change Post visit pet DTO response (remove pet id)

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

        [HttpGet("{id}")]
        [ActionName(nameof(GetVisitByIdAsync))]
        public async Task<IActionResult> GetVisitByIdAsync(int id)
        {
            var response = await _visitService.GetVisitById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchVisitsAsync([FromQuery] VisitFilterDto visitFilterDto)
        {
            var response = await _visitService.SearchVisitsAsync(visitFilterDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<VisitDto>> PostVisit(VisitPostDto visitPostDto)
        {
            var response = await _visitService.PostVisitAsync(visitPostDto);
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
