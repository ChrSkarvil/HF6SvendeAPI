using HF6Svende.Application.DTO;
using HF6Svende.Application.Service_Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HF6SvendeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingController : ControllerBase
    {
        private readonly IListingService _listingService;

        public ListingController(IListingService listingService)
        {
            _listingService = listingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllListings()
        {
            try
            {
                var listings = await _listingService.GetAllListingsAsync();
                return Ok(listings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetListingById(int id)
        {
            try
            {
                var listing = await _listingService.GetListingByIdAsync(id);
                if (listing == null)
                {
                    return NotFound("Listing not found.");
                }
                return Ok(listing);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ListingDto>> CreateListing([FromBody] CreateListingDto createListingDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdListing = await _listingService.CreateListingAsync(createListingDto);
                return CreatedAtAction(nameof(GetListingById), new { id = createdListing.Id }, createdListing);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
