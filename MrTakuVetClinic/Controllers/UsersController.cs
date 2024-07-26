using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Services;
using System.Linq;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        { 
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            if (user == null)
            { 
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("search")]
        public async Task<ActionResult<User>> GetSearchUsers([FromQuery] string firstName, string lastName)
        {
            var users = await _userService.GetSearchUsersAsync(firstName, lastName);
            if (users == null || !users.Any())
            { 
                return NotFound();
            }

            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUserByUsername), new { username = user.Username }, user);
        }

        [HttpPut]
        public async Task<IActionResult> PutUser(string username, [FromBody] User user)
        {
            if (username != user.Username)
            {
                return BadRequest();
            }

            var existingUser = await _userService.GetUserByUsernameAsync(username);
            if (existingUser == null)
            {
                return NotFound();
            }

            await _userService.UpdateUserAsync(existingUser);
            return NoContent();
        }

        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        { 
            var user = await _userService.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.DeleteUserByUsernameAsync(username);
            return NoContent();
        }

        //[HttpPut("{username}")]
        //public async Task<IActionResult> UpdateUserDetails(string username, [FromBody] User userDetails)
        //{
        //    var user = _context.Users.FirstOrDefault(u => u.Username == username);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    user.FirstName = userDetails.FirstName;
        //    user.MiddleName = userDetails.MiddleName;
        //    user.LastName = userDetails.LastName;
        //    user.Email = userDetails.Email;

        //    _context.Entry(user).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return Ok(user);
        //}
    }
}
