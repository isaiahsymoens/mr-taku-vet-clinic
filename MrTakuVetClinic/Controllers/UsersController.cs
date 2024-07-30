using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Services;
using System;
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
            try
            {
                return Ok(await _userService.GetUserByUsernameAsync(username));
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<User>> GetSearchUsers([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var users = await _userService.GetSearchUsersAsync(firstName, lastName);
            if (users == null || !users.Any())
            { 
                return NotFound(new { Message = "User not found." });
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

            try
            {
                await _userService.AddUserAsync(user);
                return CreatedAtAction(nameof(GetUserByUsername), new { username = user.Username }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> PutUser(string username, [FromBody] User user)
        {
            var existingUser = await _userService.GetUserByUsernameAsync(username);
            if (existingUser == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            await _userService.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        { 
            try
            {
                await _userService.DeleteUserByUsernameAsync(username);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
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
