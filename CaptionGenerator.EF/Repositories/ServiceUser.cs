using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Entities;
using CaptionGenerator.CORE.Interfaces;
using CaptionGenerator.EF.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CaptionGenerator.EF.Repositories
{
    public class ServiceUser : IServiceUser
    {
        private readonly ApplicationDbContext _context;

        public ServiceUser(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceDto> CreateServiceAsync(ServiceDto serviceDto)
        {
            if (serviceDto == null)
            {
                throw new ArgumentNullException(nameof(serviceDto));
            }

            // Validate TeamId
            var teamExists = await _context.Teams.AnyAsync(t => t.Id == serviceDto.TeamId);
            if (!teamExists)
            {
                // Team with the provided TeamId does not exist
                throw new ArgumentException("Team with the provided TeamId does not exist.", nameof(serviceDto.TeamId));
            }

            var service = new Service
            {
                TeamId = serviceDto.TeamId,
                Name = serviceDto.Name,
                Description = serviceDto.Description,
                NumberOfRequests = serviceDto.NumberOfRequests,
                Url = serviceDto.Url,
                ImageUrl = serviceDto.ImageUrl,
                BackgroundImageUrl = serviceDto.BackgroundImageUrl
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return serviceDto;
        }

        public async Task<bool> DeleteServiceAsync(int serviceId)
        {
            var service = await _context.Services.FindAsync(serviceId);

            if (service == null)
            {
                // Service with the provided Id does not exist
                return false;
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ServiceDto> UpdateServiceAsync(int serviceId, ServiceDto serviceDto)
        {
            if (serviceDto == null)
            {
                throw new ArgumentNullException(nameof(serviceDto));
            }

            var existingService = await _context.Services.FindAsync(serviceId);

            if (existingService == null)
            {
                // Service with the provided Id does not exist
                throw new ArgumentException("Service with the provided Id does not exist.", nameof(serviceId));
            }

            // Validate TeamId
            var teamExists = await _context.Teams.AnyAsync(t => t.Id == serviceDto.TeamId);
            if (!teamExists)
            {
                // Team with the provided TeamId does not exist
                throw new ArgumentException("Team with the provided TeamId does not exist.", nameof(serviceDto.TeamId));
            }

            // Update existingService properties
            existingService.TeamId = serviceDto.TeamId;
            existingService.Name = serviceDto.Name;
            existingService.Description = serviceDto.Description;
            existingService.NumberOfRequests = serviceDto.NumberOfRequests;
            existingService.Url = serviceDto.Url;
            existingService.ImageUrl = serviceDto.ImageUrl;
            existingService.BackgroundImageUrl = serviceDto.BackgroundImageUrl;

            await _context.SaveChangesAsync();

            return serviceDto;
        }
        public async Task<ServiceDto> GetServiceByIdAsync(int serviceId)
        {
            var service = await _context.Services
                .Where(s => s.Id == serviceId)
                .Select(s => new ServiceDto
                {
                    TeamId = s.TeamId,
                    Name = s.Name,
                    Description = s.Description,
                    NumberOfRequests = s.NumberOfRequests,
                    Url = s.Url,
                    ImageUrl = s.ImageUrl,
                    BackgroundImageUrl = s.BackgroundImageUrl
                })
                .FirstOrDefaultAsync();

            return service;
        }
    }
}
