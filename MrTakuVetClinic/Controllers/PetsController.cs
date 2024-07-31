using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.DTOs.Pet;
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
        // TODO:
        // Validation to not allow the user to delete pet record
        private readonly PetService _petService;
        private readonly UserService _userService;

        public PetsController(PetService petService, UserService userService)
        {
            _petService = petService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPetsAsync()
        {
            return Ok(await _petService.GetAllPetsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPetByIdAsync(int id)
        {
            try
            {
                return Ok(await _petService.GetPetByIdAsync(id));
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPetRecord([FromBody] PostPetDto addPet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (addPet.Username == null)
                {
                    return BadRequest("Username is required.");
                }

                var user = await _userService.GetUserWithUserIdByUsername(addPet.Username);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var pet= new Pet
                {
                    PetName = addPet.PetName,
                    PetTypeId = addPet.PetTypeId,
                    Breed = addPet.Breed,
                    BirthDate = addPet.BirthDate,
                    UserId = user.UserId
                };

                await _petService.PostPetAsync(pet);
                return Ok("Success.");
                //return CreatedAtAction(nameof(GetPetByIdAsync), new { id = pet.PetId }, pet);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePetRecordAsync(int id)
        {
            await _petService.DeletePetAsync(id);
            return NoContent();
        }
    }
}
