using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.Entities;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VisitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _context.Visits.ToListAsync() });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Visit>> GetVisitById(int id)
        {
            var visit = await _context.Visits
                .Include(v => v.VisitType)
                .FirstOrDefaultAsync(v => v.VisitId == id);

            if (visit == null)
            {
                return NotFound();
            }

            return Ok(visit);
        }

        [HttpPost]
        public IActionResult PostVisit([FromBody] Visit visit)
        {
            if (visit == null)
            {
                return BadRequest("Pet data is required");
            }

            var user = _context.Pets.Find(visit.PetId);
            if (user == null)
            {
                ModelState.AddModelError("PetId", "Invalid PetId");
                return BadRequest(ModelState);
            }

            _context.Visits.Add(visit);
            _context.SaveChanges();

            return Ok(new { Message = "Successfully added." });
        }
    }
}
