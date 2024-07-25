using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.Entities;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PetTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _context.PetTypes.ToListAsync() });
        }

        [HttpGet("{id}")]
        public ActionResult<PetType> GetPetTypeById(int id)
        {
            var petType = _context.PetTypes.FindAsync(id);
            if (petType == null)
            {
                return NotFound();
            }

            return Ok(petType);
        }

        [HttpPost]
        public async Task<IActionResult> PostPetType(PetType petType)
        {
            _context.PetTypes.Add(petType);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
