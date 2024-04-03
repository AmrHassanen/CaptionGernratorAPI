// ITeamService interface
using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaptionGenerator.CORE.Interfaces
{
    public interface ITeamService
    {
        Task<List<Team>> GetAllTeamsAsync();
        Task<Team> GetTeamByIdAsync(int teamId);
        Task<Team> CreateTeamAsync(TeamDto teamDto);
        Task<Team> UpdateTeamAsync(int teamId, TeamDto teamDto);
        Task<bool> DeleteTeamAsync(int teamId);
    }
}
