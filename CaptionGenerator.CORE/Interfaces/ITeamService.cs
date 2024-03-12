// ITeamService interface
using CaptionGenerator.CORE.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaptionGenerator.CORE.Interfaces
{
    public interface ITeamService
    {
        Task<List<TeamDto>> GetAllTeamsAsync();
        Task<TeamDto> GetTeamByIdAsync(int teamId);
        Task<TeamDto> CreateTeamAsync(TeamDto teamDto);
        Task<TeamDto> UpdateTeamAsync(int teamId, TeamDto teamDto);
        Task<bool> DeleteTeamAsync(int teamId);
    }
}
