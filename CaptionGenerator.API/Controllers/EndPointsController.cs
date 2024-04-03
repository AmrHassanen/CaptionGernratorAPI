using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CaptionGenerator.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EndpointsController : ControllerBase
    {
        private readonly IEndpointService _endpointService;

        public EndpointsController(IEndpointService endpointService)
        {
            _endpointService = endpointService ?? throw new ArgumentNullException(nameof(endpointService));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEndpoint([FromBody] EndPointDto endpointDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdEndpoint = await _endpointService.CreateEndpointAsync(endpointDto);

                return Ok(createdEndpoint);
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

        [HttpPut("{endpointId}")]
        public async Task<IActionResult> UpdateEndpoint(int endpointId, [FromBody] EndPointDto endpointDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedEndpoint = await _endpointService.UpdateEndpointAsync(endpointId, endpointDto);

                if (updatedEndpoint == null)
                {
                    return NotFound();
                }

                return Ok(updatedEndpoint);
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

        [HttpDelete("{endpointId}")]
        public async Task<IActionResult> DeleteEndpoint(int endpointId)
        {
            try
            {
                var isDeleted = await _endpointService.DeleteEndpointAsync(endpointId);

                if (isDeleted)
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

        [HttpGet("{endpointId}")]
        public async Task<IActionResult> GetEndpointById(int endpointId)
        {
            try
            {
                var endpoint = await _endpointService.GetEndpointByIdAsync(endpointId);

                if (endpoint == null)
                {
                    return NotFound();
                }

                return Ok(endpoint);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
