using HF6Svende.Application.DTO;
using HF6Svende.Application.DTO.Login;
using HF6Svende.Application.Service_Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HF6SvendeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public TokenController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginAuthDTO loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var authResponse = await _loginService.AuthenticateUserAsync(loginDto);
                return Ok(new { authResponse });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { Message = "Invalid credentials." });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request." });
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var authResponse = await _loginService.RefreshTokenAsync(tokenRequest.Token, tokenRequest.RefreshToken);
                return Ok(new { authResponse });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { Message = "Invalid token or refresh token." });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request." });
            }
        }

    }
}
