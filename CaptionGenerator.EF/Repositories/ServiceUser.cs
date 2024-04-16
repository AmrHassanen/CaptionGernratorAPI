using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Entities;
using CaptionGenerator.CORE.Interfaces;
using CaptionGenerator.EF.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Rootics.CORE.Interfaces;
using System;
using System.Threading.Tasks;

namespace CaptionGenerator.EF.Repositories
{
    public class ServiceUser : IServiceUser
    {
        private readonly ApplicationDbContext _context;
        private readonly IPhotoService _photoService;

        public ServiceUser(ApplicationDbContext context, IPhotoService photoService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _photoService = photoService ?? throw new ArgumentNullException(nameof(photoService));
        }

        public async Task<Service> CreateServiceAsync(ServiceDto serviceDto)
        {
            if (serviceDto == null)
            {
                throw new ArgumentNullException(nameof(serviceDto));
            }

            var imageUploadResult = await _photoService.AddPhotoAsync(serviceDto.ImageUrl!);
            var backgroundImageUploadResult = await _photoService.AddPhotoAsync(serviceDto.BackgroundImageUrl!);

            var service = new Service
            {
                Name = serviceDto.Name,
                Description = serviceDto.Description,
                NumberOfRequests = serviceDto.NumberOfRequests,
                ImageUrl = imageUploadResult.SecureUrl.AbsoluteUri,
                BackgroundImageUrl = backgroundImageUploadResult.SecureUrl.AbsoluteUri,
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            // Retrieve the newly created Service entity from the database
            var createdService = await _context.Services.FindAsync(service.Id);

            return createdService;
        }


        public async Task<bool> DeleteServiceAsync(int serviceId)
        {
            var service = await _context.Services.FindAsync(serviceId);

            if (service == null)
            {
                return false; // Service with the provided Id does not exist
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Service> UpdateServiceAsync(int serviceId, ServiceDto serviceDto)
        {
            if (serviceDto == null)
            {
                throw new ArgumentNullException(nameof(serviceDto));
            }

            var existingService = await _context.Services.FindAsync(serviceId);

            if (existingService == null)
            {
                throw new ArgumentException("Service with the provided Id does not exist.", nameof(serviceId));
            }

            

            var hasNewImageUrl = serviceDto.ImageUrl is not null;
            var hasBackgroundImageUrl = serviceDto.BackgroundImageUrl is not null;

            if (hasNewImageUrl)
            {
                var imageUploadResult = await _photoService.AddPhotoAsync(serviceDto.ImageUrl!);
                existingService.ImageUrl = imageUploadResult.SecureUrl.AbsoluteUri;
            }

            if (hasBackgroundImageUrl)
            {
                var backgroundImageUploadResult = await _photoService.AddPhotoAsync(serviceDto.BackgroundImageUrl!);
                existingService.BackgroundImageUrl = backgroundImageUploadResult.SecureUrl.AbsoluteUri;
            }

            existingService.Name = serviceDto.Name;
            existingService.Description = serviceDto.Description;
            existingService.NumberOfRequests = serviceDto.NumberOfRequests;


            await _context.SaveChangesAsync();

            // Retrieve the updated Service entity from the database
            var updatedService = await _context.Services.FindAsync(serviceId);

            return updatedService;
        }

        public async Task<Service> GetServiceByIdAsync(int serviceId)
        {
            var service = await _context.Services
                .Include(s => s.Teams) // Include Teams related to the Service
                    .ThenInclude(t => t.Members) // Include Members related to each Team
                .Include(s => s.EndPoints) // Include EndPoints related to the Service
                .FirstOrDefaultAsync(s => s.Id == serviceId);

            return service;
        }

        public async Task<List<Service>> GetAllServicesAsync()
        {
            var services = await _context.Services
                .Include(s => s.Teams) // Include Teams related to each Service
                    .ThenInclude(t => t.Members) // Include Members related to each Team
                .Include(s => s.EndPoints) // Include EndPoints related to each Service
                .ToListAsync();

            return services;
        }

    }
}
