﻿using Microsoft.AspNetCore.Mvc;
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
            return Ok(await _visitService.GetAllVisitsAsync());
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetVisitByIdAsync))]
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

        [HttpPost]
        public async Task<ActionResult<VisitDto>> PostVisit(VisitPostDto visitPostDto)
        {
            try
            {
                return Ok(await _visitService.PostVisitAsync(visitPostDto));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitRecordAsync(int id)
        {
            try
            {
                await _visitService.DeleteVisitAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
