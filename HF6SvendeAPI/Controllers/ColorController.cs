using HF6Svende.Application.Service_Interfaces;
using HF6Svende.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HF6SvendeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllColors()
        {
            try
            {
                var customers = await _colorService.GetAllColorsAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
