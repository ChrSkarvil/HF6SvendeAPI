using HF6Svende.Application.DTO.Login;
using HF6Svende.Application.Service_Interfaces;
using HF6Svende.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HF6SvendeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLogins()
        {
            try
            {
                var logins = await _loginService.GetAllLoginsAsync();
                return Ok(logins);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{search}")]
        public async Task<IActionResult> GetLogin(string search)
        {
            try
            {
                // Try to parse the search as an integer
                if (int.TryParse(search, out int id))
                {
                    // If it's an integer, search by ID
                    var loginById = await _loginService.GetLoginByIdAsync(id);
                    if (loginById == null)
                    {
                        return NotFound($"Could not find Login {search}");
                    }
                    return Ok(loginById);
                }
                else
                {
                    // If it's not an integer search by email
                    var loginByEmail = await _loginService.GetLoginByEmailAsync(search);
                    if (loginByEmail == null)
                    {
                        return NotFound($"Could not find Login {search}");
                    }
                    return Ok(loginByEmail);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetLoginsCount()
        {
            try
            {
                var count = await _loginService.GetLoginsCountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<LoginDTO>> CreateLogin([FromBody] LoginCreateDTO createLoginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdLogin = await _loginService.CreateLoginAsync(createLoginDto);
                return CreatedAtAction(nameof(GetLogin), new { search = createdLogin.Id }, createdLogin);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLogin(int id, [FromBody] LoginUpdateDTO updateLoginDto)
        {
            try
            {
                var updatedLogin = await _loginService.UpdateLoginAsync(id, updateLoginDto);
                if (updatedLogin == null)
                {
                    return NotFound($"Login not found {id}");
                }

                return Ok(updatedLogin);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogin(int id)
        {
            try
            {
                var success = await _loginService.DeleteLoginAsync(id);
                if (!success)
                {
                    return NotFound($"Login not found {id}");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}/{isActive}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetLoginInactive(int id, bool isActive)
        {
            try
            {
                // Call the service method to update the login's verification status
                bool isUpdated = await _loginService.SetLoginInactiveAsync(id, isActive);

                if (isUpdated)
                {
                    // Return 200 OK if the update was successful
                    return Ok(new { Success = true });
                }
                else
                {
                    // Return 404 Not Found if the login was not found
                    return NotFound(new { Success = false, Message = "Login not found." });
                }
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error with a generic error message
                return StatusCode(500, new { Success = false, Message = "An error occurred while updating the login.", Details = ex.Message });
            }
        }

    }
}
