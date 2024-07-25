using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _context.Users.ToListAsync() });
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<User>> GetUserByUsername(string username)
        {
            //var user = _context.Users.FirstOrDefault(usr => usr.Username == username);

            var user = await _context.Users
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(u => u.Username == username);

            //var user = _context.Users.Include(p => p.Pets).FirstOrDefault(usr => usr.Username == username);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("search")]
        public async Task<ActionResult<User>> SearchUsers([FromQuery] string firstName, string lastName)
        {
            var query = _context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName))
            { 
                query = query.Where(u=>
                    (string.IsNullOrEmpty(firstName) || u.FirstName.Contains(firstName)) &&
                    (string.IsNullOrEmpty(lastName) || u.LastName.Contains(lastName))
                );
            }

            var users = await query.ToListAsync();
            if (users == null || !users.Any())
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(User user)
        {
            if (user == null) { 
                return BadRequest();
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserByUsername), new { username = user.Username }, user);
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> UpdateUserDetails(string username, [FromBody] User userDetails)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = userDetails.FirstName;
            user.MiddleName = userDetails.MiddleName;
            user.LastName = userDetails.LastName;
            user.Email = userDetails.Email;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpDelete("{username}")]
        public ActionResult DeleteUserRecord(string username)
        {
            var user = _context.Users.FirstOrDefault(u=> u.Username== username);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
