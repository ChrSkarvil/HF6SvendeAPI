using HF6Svende.Application.DTO.Listing;
using HF6Svende.Application.Service_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpGet("denied")]
        public async Task<IActionResult> GetDeniedListings()
        {
            try
            {
                var listings = await _listingService.GetAllDeniedListingsAsync();
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

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetListingByCustomerId(int customerId)
        {
            try
            {
                var listing = await _listingService.GetListingsByCustomerIdAsync(customerId);
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

        [HttpGet("unverified/count")]
        public async Task<IActionResult> GetVerifiedListingCount()
        {
            try
            {
                var count = await _listingService.GetUnverifiedListingCountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("denied/count")]
        public async Task<IActionResult> GetDeniedListingCount()
        {
            try
            {
                var count = await _listingService.GetDeniedListingCountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetListingCount()
        {
            try
            {
                var count = await _listingService.GetListingCountAsync();
                return Ok(count);
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
        public async Task<IActionResult> UpdateListing(int id, [FromForm] ListingUpdateDTO updateListingDto)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized("User ID not found in token.");
                }

                var roleClaim = User.FindFirst(ClaimTypes.Role);
                string role = roleClaim?.Value ?? string.Empty;

                var customerIdClaim = User.FindFirst("CustomerId");
                int? customerId = customerIdClaim != null ? int.Parse(customerIdClaim.Value) : (int?)null;


                var updatedListing = await _listingService.UpdateListingAsync(id, updateListingDto, customerId, role);
                if (updatedListing == null)
                {
                    return NotFound("Listing not found.");
                }

                return Ok(updatedListing);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { Message = "User is not authorized to update the listing." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}/{verified}/{denyDate?}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetListingVerified(int id, bool verified, DateTime? denyDate)
        {
            try
            {
                // Call the service method to update the listing's verification status
                bool isUpdated = await _listingService.SetListingVerifiedAsync(id, verified, denyDate);

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

        [HttpPut("delete/{id}/{deleted}/{deleteDate?}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetListingDeleteDate(int id, bool deleted, DateTime? deleteDate)
        {
            try
            {
                // Call the service method to update the listing's deletedate
                bool isUpdated = await _listingService.SetListingDeleteDateAsync(id, deleted, deleteDate);

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

