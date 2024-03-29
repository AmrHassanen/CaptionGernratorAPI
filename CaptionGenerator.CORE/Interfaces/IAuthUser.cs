﻿using CaptionGenerator.CORE.Authentication;
using CaptionGenerator.CORE.Dtos;

namespace CaptionGenerator.CORE.Interfaces
{
    public interface IAuthUser
    {
        Task<CaptionGeneratorUser> RegisterAsync(RegisterModelDto registerModelDto);
        Task<CaptionGeneratorUser> GetTokenAsync(GetTokenRequstDto getTokenRequstDto);
        Task<string> AddRoleAsync(RoleModelDto roleModel);
        Task<bool> ForgetPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);

    }
}
