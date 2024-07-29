using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Services;
using System;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedsController : Controller
    {
        private readonly BreedService _breedService;
        public BreedsController(BreedService breedService)
        {
            _breedService = breedService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBreedsAsync()
        {
            return Ok(await _breedService.GetAllBreedsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBreedById(int id)
        {
            try
            {
                return Ok(await _breedService.GetBreedByIdAsync(id));
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostBreed(Breed breed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _breedService.PostBreedAsync(breed);
            return CreatedAtAction(nameof(GetBreedById), new { id = breed.BreedId}, breed);
        }
    }
}
