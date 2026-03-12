using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModularAuth.Application.DTOs.User;
using ModularAuth.Application.Interfaces;

namespace ModularAuth.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var result = await _authService.RegisterAsync(request);

            if (result.IsFailure)
            {
                if (result.ErrorCode == "409")
                    return Conflict(new { Error = result.Error });
                return BadRequest(new { Error = result.Error });
            }
            return Ok(result.Value);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _authService.LoginAsync(request);

            if (result.IsFailure)
            {
                // Handle specific errors (e.g., Invalid credentials)
                if (result.ErrorCode == "401")
                    return Unauthorized(new { Error = result.Error });
                
                return BadRequest(new { Error = result.Error });
            }

            return Ok(result.Value);
        }
    }

}
