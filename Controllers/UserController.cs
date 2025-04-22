using Apotek.DTO;
using Apotek.Helpers;
using Apotek.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Apotek.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // 🔐 LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto request)
        {
            var response = await _userService.Login(request);
            return StatusCode(response.StatusCode, response);
        }

        // 🔐 GET ME
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdStr == null)
                return Unauthorized(new ResponseAPI { Status = false, Message = "Token tidak valid" });

            if (!int.TryParse(userIdStr, out int userId))
                return Unauthorized(new ResponseAPI { Status = false, Message = "User ID tidak valid" });

            var response = await _userService.GetById(userId);
            return StatusCode(response.StatusCode, response);
        }

        // 📄 GET ALL USERS (with search & pagination)
        [HttpPost("all")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAll([FromBody] SearchDto request)
        {
            var response = await _userService.GetAll(request);
            return StatusCode(response.StatusCode, response);
        }

        // 📄 GET USER BY ID
        [HttpPost("getById")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetByIdFromBody([FromBody] UserDto request)
        {
            var response = await _userService.GetById(request.Id);
            return StatusCode(response.StatusCode, response);
        }

        // ➕ CREATE USER
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Register([FromBody] UserDto request)
        {
            var response = await _userService.Register(request);
            return StatusCode(response.StatusCode, response);
        }

        // ✏️ UPDATE USER
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update([FromBody] UserDto request)
        {
            var response = await _userService.UpdateUser(request);
            return StatusCode(response.StatusCode, response);
        }

        // ❌ DELETE USER
        [HttpPost("delete")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteByBody([FromBody] UserDto request)
        {
            var response = await _userService.Delete(request);
            return StatusCode(response.StatusCode, response);
        }

    }
}
