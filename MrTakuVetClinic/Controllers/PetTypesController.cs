using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.DTOs.PetType;
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
            var response = await _petTypeService.GetAllPetTypesAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPetTypeByIdAsync(int id)
        {
            var response = await _petTypeService.GetPetTypeByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> PostPetTypeAsync(PetTypePostDto petTypePostDto)
        {
            var response = await _petTypeService.PostPetTypeAsync(petTypePostDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPetTypeAsync(int id, [FromBody] PetTypeUpdateDto petTypeUpdateDto)
        {
            var response = await _petTypeService.UpdatePetTypeAsync(id, petTypeUpdateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePetTypeAsync(int id)
        {
            var response = await _petTypeService.DeletePetTypeAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
