using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.DTOs.User;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Models;
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
        public async Task<IActionResult> PostUser(UserPostDto userPostDto)
        {
            var response = await _userService.PostUserAsync(userPostDto);
            return StatusCode(response.StatusCode, response);
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
