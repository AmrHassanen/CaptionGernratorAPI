using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Interfaces;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthUser _authUserService;

    public AuthController(IAuthUser authUserService)
    {
        _authUserService = authUserService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModelDto registerModelDto)
    {
        var result = await _authUserService.RegisterAsync(registerModelDto);

        if (result.IsAuthenticated)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(new { Message = result.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] GetTokenRequstDto getTokenRequstDto)
    {
        var result = await _authUserService.GetTokenAsync(getTokenRequstDto);

        if (result.IsAuthenticated)
        {
            return Ok(result);
        }
        else
        {
            return Unauthorized(new { Message = result.Message });
        }
    }

    [HttpPost("addrole")]
    [Authorize(Roles = "Admin")] // Only accessible to users with the "Admin" role
    public async Task<IActionResult> AddRole([FromBody] RoleModelDto roleModel)
    {
        var result = await _authUserService.AddRoleAsync(roleModel);

        if (string.IsNullOrEmpty(result))
        {
            return Ok(new { Message = "Role added successfully." });
        }
        else
        {
            return BadRequest(new { Message = result });
        }
    }

    [HttpPost("forgetpassword")]
    public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordDto forgetPasswordDto)
    {
        var result = await _authUserService.ForgetPasswordAsync(forgetPasswordDto.Email);

        if (result)
        {
            return Ok(new { Message = "Password reset link sent successfully." });
        }
        else
        {
            return BadRequest(new { Message = "Failed to send the reset link." });
        }
    }

    [HttpPost("resetpassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
    {
        var result = await _authUserService.ResetPasswordAsync(resetPasswordDto);

        if (result)
        {
            return Ok(new { Message = "Password reset successfully." });
        }
        else
        {
            return BadRequest(new { Message = "Failed to reset the password." });
        }
    }

}
