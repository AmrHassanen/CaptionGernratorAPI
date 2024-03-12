// TeamService implementation
using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Entities;
using CaptionGenerator.CORE.Interfaces;
using CaptionGenerator.EF.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaptionGenerator.EF.Services
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext _context;

        public TeamService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TeamDto>> GetAllTeamsAsync()
        {
            var teams = await _context.Teams
                .Select(t => new TeamDto
                {
                    MemberIds = t.MemberIds,
                    ImageUrl = t.ImageUrl,
                    BackgroundImageUrl = t.BackgroundImageUrl
                })
                .ToListAsync();

            return teams;
        }

        public async Task<TeamDto> GetTeamByIdAsync(int teamId)
        {
            var team = await _context.Teams
                .Where(t => t.Id == teamId)
                .Select(t => new TeamDto
                {
                    MemberIds = t.MemberIds,
                    ImageUrl = t.ImageUrl,
                    BackgroundImageUrl = t.BackgroundImageUrl
                })
                .FirstOrDefaultAsync();

            return team;
        }

        public async Task<TeamDto> CreateTeamAsync(TeamDto teamDto)
        {
            var team = new Team
            {
                MemberIds = teamDto.MemberIds,
                ImageUrl = teamDto.ImageUrl,
                BackgroundImageUrl = teamDto.BackgroundImageUrl
            };

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();


            return teamDto;
        }

        public async Task<TeamDto> UpdateTeamAsync(int teamId, TeamDto teamDto)
        {
            var existingTeam = await _context.Teams.FindAsync(teamId);

            if (existingTeam == null)
            {
                throw new ArgumentException("Team with the provided Id does not exist.", nameof(teamId));
            }

            existingTeam.MemberIds = teamDto.MemberIds;
            existingTeam.ImageUrl = teamDto.ImageUrl;
            existingTeam.BackgroundImageUrl = teamDto.BackgroundImageUrl;

            await _context.SaveChangesAsync();

            return teamDto;
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
