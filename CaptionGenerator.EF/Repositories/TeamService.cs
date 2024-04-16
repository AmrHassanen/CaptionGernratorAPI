using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Entities;
using CaptionGenerator.CORE.Interfaces;
using CaptionGenerator.EF.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Rootics.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;

namespace CaptionGenerator.EF.Repositories
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPhotoService _photoService;

        public TeamService(ApplicationDbContext context, IPhotoService photoService)
        {
            _context = context;
            _photoService = photoService;
        }

        public async Task<List<Team>> GetAllTeamsAsync()
        {
             var teams = await _context.Teams
            .Include(t => t.Members) // Include members
            .ToListAsync();
             return teams;
        }

        public async Task<Team> GetTeamByIdAsync(int teamId)
        {
            var team = await _context.Teams
                .Include(t => t.Members)
                .FirstOrDefaultAsync(t => t.Id == teamId);

            return team;
        }

        public async Task<Team> CreateTeamAsync(TeamDto teamDto)
        {
            var imageUploadResult = await _photoService.AddPhotoAsync(teamDto.ImageUrl!);
            var backgroundImageUploadResult = await _photoService.AddPhotoAsync(teamDto.BackgroundImageUrl!);

            var team = new Team
            {
                ImageUrl = imageUploadResult.SecureUrl.AbsoluteUri, // Assuming Url property contains the image URL
                BackgroundImageUrl = backgroundImageUploadResult.SecureUrl.AbsoluteUri,
                Description = teamDto.Description,
                Name = teamDto.Name,
                ServiceId = teamDto.ServiceId,
            };

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return team; // Return the newly created team entity
        }

        public async Task<Team> UpdateTeamAsync(int teamId, TeamDto teamDto)
        {
            var existingTeam = await _context.Teams.FindAsync(teamId);

            if (existingTeam == null)
            {
                throw new ArgumentException("Team with the provided Id does not exist.", nameof(teamId));
            }

            var hasNewImageUrl = teamDto.ImageUrl is not null;
            var hasNewBackgroundImageUrl = teamDto.BackgroundImageUrl is not null;


            if (hasNewImageUrl)
            {
                var imageUploadResult = await _photoService.AddPhotoAsync(teamDto.ImageUrl!);
                existingTeam.ImageUrl = imageUploadResult.SecureUrl.AbsoluteUri; // Assuming Url property contains the image URL
            }

            if (hasNewBackgroundImageUrl)
            {
                var backgroundImageUploadResult = await _photoService.AddPhotoAsync(teamDto.BackgroundImageUrl!);
                existingTeam.BackgroundImageUrl = backgroundImageUploadResult.SecureUrl.AbsoluteUri;
            }
            existingTeam.Description = teamDto.Description;
            existingTeam.Name = teamDto.Name;
            existingTeam.ServiceId = teamDto.ServiceId;

            _context.Update(existingTeam);
            await _context.SaveChangesAsync();

            return existingTeam; // Return the updated team entity
        }

        public async Task<bool> DeleteTeamAsync(int teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);

            if (team == null)
            {
                return false; // Team with the provided Id does not exist
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
