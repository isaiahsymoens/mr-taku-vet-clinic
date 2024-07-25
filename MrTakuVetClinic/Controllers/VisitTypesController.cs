using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.Entities;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public VisitTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _context.VisitsTypes.ToListAsync() });
        }

        [HttpGet("{id}")]
        public ActionResult<PetType> GetVisitTypeById(int id)
        {
            var petType = _context.VisitsTypes.FindAsync(id);
            if (petType == null)
            {
                return NotFound();
            }

            return Ok(petType);
        }

        [HttpPost]
        public async Task<IActionResult> PostVisitType(VisitType visitType)
        {
            _context.VisitsTypes.Add(visitType);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
