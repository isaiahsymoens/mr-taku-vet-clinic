using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.Entities;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Controllers
{
    [Route("api/pets")]
    [ApiController]
    public class PetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _context.Pets.ToListAsync() });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetPetById(int id)
        {
            var pet = await _context.Pets
                .Include(u => u.PetType)
                .Include(u => u.Breed)
                .FirstOrDefaultAsync(u => u.PetId == id);

            if (pet == null)
            {
                return NotFound();
            }

            return Ok(pet);
        }

        [HttpPost]
        public IActionResult AddPetRecord([FromBody] Pet pet)
        {
            if (pet == null)
            {
                return BadRequest("Pet data is required");
            }

            var user = _context.Users.Find(pet.UserId);
            if (user == null)
            {
                ModelState.AddModelError("UserId", "Invalid UserId");
                return BadRequest(ModelState);
            }

            _context.Pets.Add(pet);
            _context.SaveChanges();

            return Ok(new { Message = "Successfully added." });
        }
    }
}
