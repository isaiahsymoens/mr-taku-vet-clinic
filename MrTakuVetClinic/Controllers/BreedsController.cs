using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.Entities;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BreedsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _context.Breeds.ToListAsync() });
        }

        [HttpGet("{id}")]
        public ActionResult<Breed> GetBreedById(int id)
        {
            var breed = _context.Breeds.FindAsync(id);
            if (breed == null)
            {
                return NotFound();
            }

            return Ok(breed);
        }

        [HttpPost]
        public async Task<IActionResult> PostBreed(Breed breed)
        {
            _context.Breeds.Add(breed);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
