using Microsoft.AspNetCore.Mvc;
using FOODCMS.API.Dtos.Auth;
using FOODCMS.API.Services;
using FOODCMS.API.Entities;
using FOODCMS.API.Dtos;

namespace FOODCMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _authService.LoginAsync(request);
            if (user == null)
                return Unauthorized("Username or password invalid");

            // ตรงนี้ค่อย gen JWT token ถ้ามี
            // var token = _jwtTokenHelper.GenerateToken(user);

            return Ok(new {
                //user.Id,
                user.Username,
                // Token = token
            });
        }
}
