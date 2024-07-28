using Microsoft.AspNetCore.Mvc;
using MrTakuVetClinic.Entities;
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
            return Ok(await _userTypeService.GetAllUserTypesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserTypeById(int id)
        {
            var userType = await _userTypeService.GetUserTypeByIdAsync(id);
            if (userType == null)
            {
                return NotFound();
            }

            return Ok(userType);
        }

        [HttpPost]
        public async Task<IActionResult> PostUserTypeAsync(UserType userType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userTypeService.PostUserTypeAsync(userType);
            return CreatedAtAction(nameof(GetUserTypeById), new { id = userType.UserTypeId }, userType);
        }
    }
}
