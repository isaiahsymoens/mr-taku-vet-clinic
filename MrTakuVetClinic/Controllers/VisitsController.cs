using Microsoft.AspNetCore.Mvc;
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
            return Ok(await _visitService.GetAllVisitsAsync());
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchVisitsAsync([FromQuery] VisitFilterDto visitFilterDto)
        {
            try
            {
                var visits = await _visitService.SearchVisitsAsync(visitFilterDto);
                return Ok(visits);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVisitByIdAsync(int id)
        {
            try
            {
                return Ok(await _visitService.GetVisitById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostVisit([FromBody] Visit visit)
        {
            if (visit == null)
            {
                return BadRequest(new { Message = "Visit data is required." });
            }

            try
            {
                await _visitService.PostVisitAsync(visit);
                return Ok("success test.");
                //return CreatedAtAction(nameof(GetVisitByIdAsync), new { id = visit.VisitId }, visit);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        //    [HttpGet("{id}")]
        //    public async Task<ActionResult<Visit>> GetVisitById(int id)
        //    {
        //        var visit = await _context.Visits
        //            .Include(v => v.VisitType)
        //            .FirstOrDefaultAsync(v => v.VisitId == id);

        //        if (visit == null)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(visit);
        //    }

        //    [HttpPost]
        //    public IActionResult PostVisit([FromBody] Visit visit)
        //    {
        //        if (visit == null)
        //        {
        //            return BadRequest("Pet data is required");
        //        }

        //        var user = _context.Pets.Find(visit.PetId);
        //        if (user == null)
        //        {
        //            ModelState.AddModelError("PetId", "Invalid PetId");
        //            return BadRequest(ModelState);
        //        }

        //        _context.Visits.Add(visit);
        //        _context.SaveChanges();

        //        return Ok(new { Message = "Successfully added." });
        //    }
        //}
    }
}
