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
        public async Task<IActionResult> GetPetTypeById(int id)
        {
            var petType = await _petTypeService.GetPetTypeByIdAsync(id);
            if (petType == null)
            {
                return NotFound();
            }

            return Ok(petType);
        }

        [HttpPost]
        public async Task<IActionResult> PostPetTypeAsync(PetType petType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _petTypeService.PostPetTypeAsync(petType);
            return CreatedAtAction(nameof(GetPetTypeById), new { id = petType.PetTypeId}, petType);
        }
    }
}
