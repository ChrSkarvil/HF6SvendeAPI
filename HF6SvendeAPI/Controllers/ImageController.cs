using HF6Svende.Application.DTO.Image;
using HF6Svende.Application.Service_Interfaces;
using HF6Svende.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HF6SvendeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            try
            {
                var image = await _imageService.GetAllImagesAsync();
                return Ok(image);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImageById(int id)
        {
            try
            {
                var image = await _imageService.GetImageByIdAsync(id);
                if (image == null)
                {
                    return NotFound("Image not found.");
                }
                return Ok(image);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ImageDTO>> CreateImage([FromForm] ImageCreateDTO createImageDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdImage = await _imageService.CreateImageAsync(createImageDto);
                return CreatedAtAction(nameof(GetImageById), new { id = createdImage.Id }, createdImage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateImage(int id, [FromBody] ImageUpdateDTO updateImageDto)
        {
            try
            {
                var updatedImage = await _imageService.UpdateImageAsync(id, updateImageDto);
                if (updatedImage == null)
                {
                    return NotFound("Image not found.");
                }

                return Ok(updatedImage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            try
            {
                var success = await _imageService.DeleteImageAsync(id);
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
