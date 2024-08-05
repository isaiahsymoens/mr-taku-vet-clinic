using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.DTOs.Pet;
using MrTakuVetClinic.Services;
using System;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : Controller
    {
        private readonly PetService _petService;

        public PetsController(PetService petService)
        {
            _petService = petService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPetsAsync()
        {
            var response = await _petService.GetAllPetsAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPetByIdAsync(int id)
        {
            var response = await _petService.GetPetByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<PetDto>> AddPetRecord([FromBody] PetPostDto petPostDto)
        {
            try
            {
                return Ok(await _petService.PostPetAsync(petPostDto));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePetRecordById(int id, [FromBody] PetUpdateDto petUpdateDto)
        {
            try
            {
                await _petService.UpdatePetByIdAsync(id, petUpdateDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePetRecordAsync(int id)
        {
            var response = await _petService.DeletePetAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
