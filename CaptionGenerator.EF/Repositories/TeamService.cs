// TeamService implementation
using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Entities;
using CaptionGenerator.CORE.Interfaces;
using CaptionGenerator.EF.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaptionGenerator.EF.Repositories
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagePath;

        public TeamService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagePath = $"{_webHostEnvironment.WebRootPath}/assets/images/teams";
        }

        public async Task<List<Team>> GetAllTeamsAsync()
        {
            var teams = await _context.Teams.ToListAsync();
            return teams;
        }
        public async Task<Team> GetTeamByIdAsync(int teamId)
        {
            var team = await _context.Teams
                .Include(t => t.Members) // Include related Members if needed
                .FirstOrDefaultAsync(t => t.Id == teamId);

            return team;
        }

        public async Task<Team> CreateTeamAsync(TeamDto teamDto)
        {
            var imageTeamName = await SaveCover(teamDto.ImageUrl!);
            var imageBackgroundUrl = await SaveCover(teamDto.BackgroundImageUrl!);

            var team = new Team
            {
                MemberIds = teamDto.MemberIds,
                ImageUrl = imageTeamName,
                BackgroundImageUrl = imageBackgroundUrl
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

            existingTeam.MemberIds = teamDto.MemberIds;

            if (hasNewImageUrl)
            {
                existingTeam.ImageUrl = await SaveCover(teamDto.ImageUrl!);
            }
            if (hasNewBackgroundImageUrl)
            {
                existingTeam.BackgroundImageUrl = await SaveCover(teamDto.BackgroundImageUrl!);
            }

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
