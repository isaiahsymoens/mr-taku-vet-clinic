using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Services;
using System;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetTypesController : Controller
    {
        private PetTypeService _petTypeService;

        public PetTypesController(PetTypeService petTypeService)
        {
            _petTypeService = petTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPetTypesAsync()
        {
            return Ok(await _petTypeService.GetAllPetTypesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPetTypeByIdAsync(int id)
        {
            try
            {
                return Ok(await _petTypeService.GetPetTypeByIdAsync(id));
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostPetTypeAsync(PetType petType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _petTypeService.PostPetTypeAsync(petType);
            return Ok("Success.");
            //return CreatedAtAction(nameof(GetPetTypeByIdAsync), new { id = petType.PetTypeId}, petType);
        }
    }
}
