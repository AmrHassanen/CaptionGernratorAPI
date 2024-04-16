using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Interfaces;
using CaptionGenerator.EF.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaptionGenerator.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IMemberService _memberService;

        public TeamController(ITeamService teamService, IMemberService memberService)
        {
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeams()
        {
                var teams = await _teamService.GetAllTeamsAsync();
                return Ok(teams);          
        }

        [HttpGet("{teamId}")]
        public async Task<IActionResult> GetTeamById(int teamId)
        {
            try
            {
                var team = await _teamService.GetTeamByIdAsync(teamId);

                if (team == null)
                {
                    return NotFound();
                }

                return Ok(team);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromForm] TeamDto teamDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdTeam = await _teamService.CreateTeamAsync(teamDto);
                return Ok(createdTeam);
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


        [HttpPut("{teamId}")]
        public async Task<IActionResult> UpdateTeam(int teamId, [FromForm] TeamDto teamDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedTeam = await _teamService.UpdateTeamAsync(teamId, teamDto);

                if (updatedTeam == null)
                {
                    return NotFound();
                }

                return Ok(updatedTeam);
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

        [HttpDelete("{teamId}")]
        public async Task<IActionResult> DeleteTeam(int teamId)
        {
            try
            {
                var isDeleted = await _teamService.DeleteTeamAsync(teamId);

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
    }
}
