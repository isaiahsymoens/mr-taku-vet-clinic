using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.DTOs.UserType;
using MrTakuVetClinic.Services;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypesController : Controller
    {
        private readonly UserTypeService _userTypeService;

        public UserTypesController(UserTypeService userTypeService)
        {
            _userTypeService = userTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserTypesAsync()
        {
            var response = await _userTypeService.GetAllUserTypesAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserTypeById(int id)
        {
            var response = await _userTypeService.GetUserTypeByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> PostUserTypeAsync(UserTypePostDto userTypePostDto)
        {
            var response = await _userTypeService.PostUserTypeAsync(userTypePostDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserTypeAsync(int id, [FromBody] UserTypeUpdateDto userTypeUpdateDto)
        {
            var response = await _userTypeService.UpdateUserTypeAsync(id, userTypeUpdateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserTypeAsync(int id)
        {
            var response = await _userTypeService.DeleteUserTypeAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
