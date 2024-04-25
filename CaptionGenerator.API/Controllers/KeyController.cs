using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Interfaces;
using CaptionGenerator.EF.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CaptionGenerator.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KeyController : ControllerBase
    {
        private readonly IkeyService _keyService;

        public KeyController(IkeyService keyService)
        {
            _keyService = keyService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetKeysByUserId(string userId)
        {
            try
            {
                var keys = await _keyService.GetKeysByUserIdAsync(userId);

                if (keys == null)
                {
                    return NotFound();
                }

                return Ok(keys);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateKey([FromBody] KeyDto keyDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Get the user's ID from the HttpContext.User property
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var key = await _keyService.CreateKeyAsync(keyDto, userId);

                return Ok(key);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpPut("{keyId}")]
        public async Task<IActionResult> UpdateKey(int keyId, [FromBody] KeyUpdateRequest keyUpdateRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var key = await _keyService.UpdateKeyAsync(keyId, keyUpdateRequest);

                if (key == null)
                {
                    return NotFound();
                }

                return Ok(key);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{keyId}")]
        public async Task<IActionResult> DeleteKey(int keyId)
        {
            try
            {
                var result = await _keyService.DeleteKeyAsync(keyId);

                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }

}
