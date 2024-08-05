using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> PostPetTypeAsync(PetType petType)
        {
            var response = await _petTypeService.PostPetTypeAsync(petType);
            return StatusCode(response.StatusCode, response);
        }
    }
}
