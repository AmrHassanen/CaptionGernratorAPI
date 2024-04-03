using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Entities;
using CaptionGenerator.CORE.Interfaces;
using CaptionGenerator.EF.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CaptionGenerator.EF.Repositories
{
    public class ServiceUser : IServiceUser
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagePath;

        public ServiceUser(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagePath = $"{_webHostEnvironment.WebRootPath}/assets/images/services";
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
            var imageTeamName = await SaveCover(serviceDto.ImageUrl!);
            var imageBackgroundUrl = await SaveCover(serviceDto.BackgroundImageUrl!);

            var service = new Service
            {
                TeamId = serviceDto.TeamId,
                Name = serviceDto.Name,
                Description = serviceDto.Description,
                NumberOfRequests = serviceDto.NumberOfRequests,
                ImageUrl = imageTeamName,
                BackgroundImageUrl = imageBackgroundUrl
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

            var hasNewImageUrl = serviceDto.ImageUrl is not null;
            var hasNewBackgroundImageUrl = serviceDto.BackgroundImageUrl is not null;

            // Update existingService properties
            existingService.TeamId = serviceDto.TeamId;
            existingService.Name = serviceDto.Name;
            existingService.Description = serviceDto.Description;
            existingService.NumberOfRequests = serviceDto.NumberOfRequests;

            if (hasNewImageUrl)
            {
                existingService.ImageUrl = await SaveCover(serviceDto.ImageUrl!);
            }
            if (hasNewBackgroundImageUrl)
            {
                existingService.BackgroundImageUrl = await SaveCover(serviceDto.BackgroundImageUrl!);
            }

            await _context.SaveChangesAsync();

            return serviceDto;
        }
        public async Task<Service> GetServiceByIdAsync(int serviceId)
        {
            // Retrieve the Service entity by its ID
            var service = await _context.Services.FirstOrDefaultAsync(s => s.Id == serviceId);

            // Check if the service exists
            if (service == null)
            {
                throw new ArgumentException("Service with the provided ID does not exist.", nameof(serviceId));
            }

            return service;
        }
        private async Task<string> SaveCover(IFormFile file)
        {
            var imageUrlName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var imageUrlPath = Path.Combine(_imagePath, imageUrlName);
            using var imageUrlStream = File.Create(imageUrlPath);
            await file.CopyToAsync(imageUrlStream);
            return imageUrlName;
        }
    }
}
