﻿using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CaptionGenerator.API.Controllers
{
    [ApiController]
    [Route("api/services")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceUser _serviceUser;

        public ServicesController(IServiceUser serviceUser)
        {
            _serviceUser = serviceUser ?? throw new ArgumentNullException(nameof(serviceUser));
        }

        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] ServiceDto serviceDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Additional input validation if needed

                var createdService = await _serviceUser.CreateServiceAsync(serviceDto);

                return Ok(createdService);
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


        [HttpDelete("{serviceId}")]
        public async Task<IActionResult> DeleteService(int serviceId)
        {
            try
            {
                // Additional conditions or authorization checks if needed

                var isDeleted = await _serviceUser.DeleteServiceAsync(serviceId);

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

        [HttpPut("{serviceId}")]
        public async Task<IActionResult> UpdateService(int serviceId, [FromBody] ServiceDto serviceDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Additional input validation if needed

                var updatedService = await _serviceUser.UpdateServiceAsync(serviceId, serviceDto);

                return Ok(updatedService);
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

        [HttpGet("{serviceId}")]
        public async Task<IActionResult> GetService(int serviceId)
        {
            try
            {
                // Additional conditions or authorization checks if needed

                var serviceDto = await _serviceUser.GetServiceByIdAsync(serviceId);

                if (serviceDto == null)
                {
                    return NotFound();
                }

                return Ok(serviceDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
