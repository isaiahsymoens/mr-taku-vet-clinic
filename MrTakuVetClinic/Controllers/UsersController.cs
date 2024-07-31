using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.DTOs.User;
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
        // TODO:
        // Refactor update endpoint

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
        public async Task<IActionResult> PutUser(string username, [FromBody] UserUpdateDto user)
        {
            try
            {
                await _userService.UpdateUserAsync(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
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
    }
}
