using HF6Svende.Application.DTO.Listing;
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
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("verified")]
        public async Task<IActionResult> GetVerifiedListings()
        {
            try
            {
                var listings = await _listingService.GetAllVerifiedListingsAsync();
                return Ok(listings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("unverified")]
        public async Task<IActionResult> GetUnverifiedListings()
        {
            try
            {
                var listings = await _listingService.GetAllUnverifiedListingsAsync();
                return Ok(listings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ListingDTO>> CreateListing([FromForm] ListingCreateDTO createListingDto)
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
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateListing(int id, [FromBody] ListingUpdateDTO updateListingDto)
        {
            try
            {
                var updatedListing = await _listingService.UpdateListingAsync(id, updateListingDto);
                if (updatedListing == null)
                {
                    return NotFound("Listing not found.");
                }

                return Ok(updatedListing);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}/{verified}")]
        public async Task<IActionResult> SetListingVerified(int id, bool verified)
        {
            try
            {
                // Call the service method to update the listing's verification status
                bool isUpdated = await _listingService.SetListingVerifiedAsync(id, verified);

                if (isUpdated)
                {
                    // Return 200 OK if the update was successful
                    return Ok(new { Success = true });
                }
                else
                {
                    // Return 404 Not Found if the listing was not found
                    return NotFound(new { Success = false, Message = "Listing not found." });
                }
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error with a generic error message
                return StatusCode(500, new { Success = false, Message = "An error occurred while updating the listing.", Details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListing(int id)
        {
            try
            {
                var success = await _listingService.DeleteListingAsync(id);
                if (!success)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
