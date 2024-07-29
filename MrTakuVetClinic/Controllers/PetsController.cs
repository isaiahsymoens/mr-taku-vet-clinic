using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.Entities;
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
            return Ok(await _petService.GetAllPetsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPetById(int id)
        {
            var pet = await _petService.GetPetByIdAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            return Ok(pet);
        }

        [HttpPost]
        public async Task<IActionResult> AddPetRecord([FromBody] Pet pet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _petService.PostPetAsync(pet);
                return CreatedAtAction(nameof(GetPetById), new { id = pet.PetId }, pet);
            }
            catch (Exception ex)
            { 
                return BadRequest(new { Message = ex.Message });
            }
        }

        //[HttpPost]
        //public IActionResult AddPetRecord([FromBody] Pet pet)
        //{
        //    if (pet == null)
        //    {
        //        return BadRequest("Pet data is required");
        //    }

        //    var user = _context.Users.Find(pet.UserId);
        //    if (user == null)
        //    {
        //        ModelState.AddModelError("UserId", "Invalid UserId");
        //        return BadRequest(ModelState);
        //    }

        //    _context.Pets.Add(pet);
        //    _context.SaveChanges();

        //    return Ok(new { Message = "Successfully added." });
        //}
    }
}
