// EndpointService implementation
using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Entities;
using CaptionGenerator.CORE.Interfaces;
using CaptionGenerator.EF.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CaptionGenerator.EF.Services
{
    public class EndpointService : IEndpointService
    {
        private readonly ApplicationDbContext _context;

        public EndpointService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<EndPointDto> CreateEndpointAsync(EndPointDto endpointDto)
        {
            if (endpointDto == null)
            {
                throw new ArgumentNullException(nameof(endpointDto));
            }

            // Validate ServiceId
            var serviceExists = await _context.Services.AnyAsync(s => s.Id == endpointDto.ServiceId);
            if (!serviceExists)
            {
                throw new ArgumentException("Service with the provided ServiceId does not exist.", nameof(endpointDto.ServiceId));
            }

            var endpoint = new EndPoint
            {
                Url = endpointDto.Url,
                Body = endpointDto.Body,
                WaysToUse = endpointDto.WaysToUse,
                ServiceId = endpointDto.ServiceId
            };

            _context.EndPoints.Add(endpoint);
            await _context.SaveChangesAsync();
            

            return endpointDto;
        }

        public async Task<EndPointDto> UpdateEndpointAsync(int endpointId, EndPointDto endpointDto)
        {
            if (endpointDto == null)
            {
                throw new ArgumentNullException(nameof(endpointDto));
            }

            var existingEndpoint = await _context.EndPoints.FindAsync(endpointId);

            if (existingEndpoint == null)
            {
                throw new ArgumentException("Endpoint with the provided Id does not exist.", nameof(endpointId));
            }

            // Validate ServiceId if provided
            if (endpointDto.ServiceId != 0)
            {
                var serviceExists = await _context.Services.AnyAsync(s => s.Id == endpointDto.ServiceId);
                if (!serviceExists)
                {
                    throw new ArgumentException("Service with the provided ServiceId does not exist.", nameof(endpointDto.ServiceId));
                }
            }

            existingEndpoint.Url = endpointDto.Url;
            existingEndpoint.Body = endpointDto.Body;
            existingEndpoint.WaysToUse = endpointDto.WaysToUse;

            if (endpointDto.ServiceId != 0)
            {
                existingEndpoint.ServiceId = endpointDto.ServiceId;
            }

            await _context.SaveChangesAsync();

            return endpointDto;
        }

        public async Task<bool> DeleteEndpointAsync(int endpointId)
        {
            var endpoint = await _context.EndPoints.FindAsync(endpointId);

            if (endpoint == null)
            {
                return false; // Endpoint with the provided Id does not exist
            }

            _context.EndPoints.Remove(endpoint);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<EndPointDto> GetEndpointByIdAsync(int endpointId)
        {
            var endpoint = await _context.EndPoints
                .FirstOrDefaultAsync(e => e.Id == endpointId);

            if (endpoint == null)
            {
                return null; // Endpoint with the provided Id does not exist
            }

            var endpointDto = new EndPointDto
            {
                Url = endpoint.Url,
                Body = endpoint.Body,
                WaysToUse = endpoint.WaysToUse,
                ServiceId = endpoint.ServiceId
            };

            return endpointDto;
        }
    }
}
