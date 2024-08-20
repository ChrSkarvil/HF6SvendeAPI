using HF6Svende.Application.Service_Interfaces;
using HF6Svende.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HF6SvendeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            try
            {
                var customers = await _countryService.GetAllCountriesAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{search}")]
        public async Task<IActionResult> GetCountry(string search)
        {
            try
            {
                // Try to parse the search as an integer
                if (int.TryParse(search, out int id))
                {
                    // If it's an integer, search by ID
                    var countryById = await _countryService.GetCountryByIdAsync(id);
                    if (countryById == null)
                    {
                        return NotFound("Country not found by ID");
                    }
                    return Ok(countryById);
                }
                else
                {
                    // If it's not an integer search by name
                    var countryByName = await _countryService.GetCountryByNameAsync(search);
                    if (countryByName == null)
                    {
                        return NotFound("Country not found by name");
                    }
                    return Ok(countryByName);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
