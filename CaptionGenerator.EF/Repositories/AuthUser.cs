using Azure.Core;
using Azure;
using CaptionGenerator.CORE.Authentication;
using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Entities;
using CaptionGenerator.CORE.Interfaces;
using CaptionGenerator.EF.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net;

namespace HealthTracker.EF.Repositories
{
    public class AuthUser : IAuthUser
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthUser(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        private readonly ILogger<AuthUser> _logger;


        public AuthUser(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt,ILogger<AuthUser> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            _logger = logger;
        }
        public async Task<CaptionGeneratorUser> RegisterAsync(RegisterModelDto registerModelDto)
        {
            if (await _userManager.FindByEmailAsync(registerModelDto.Email) != null)
            {
                return new CaptionGeneratorUser { Message = "Email Is Already Register!" };
            }
            if (await _userManager.FindByEmailAsync(registerModelDto.UserName) != null)
            {
                return new CaptionGeneratorUser { Message = "UserName Is Already Register!" };
            }
            var User = new ApplicationUser
            {
                FirstName = registerModelDto.FirstName,
                LastName = registerModelDto.LastName,
                Email = registerModelDto.Email,
                UserName = registerModelDto.UserName,
            };
            var Result = await _userManager.CreateAsync(User, registerModelDto.Password);
            if (!Result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in Result.Errors)
                {
                    errors += $"{error.Description},";
                }
                return new CaptionGeneratorUser { Message = errors };
            }
            await _userManager.AddToRoleAsync(User, "User");
            var jwtSecurityToken = await CreateJwtToken(User);
            return new CaptionGeneratorUser
            {
                Email = User.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = User.UserName,
            };

        }

        public async Task<CaptionGeneratorUser> GetTokenAsync(GetTokenRequstDto getTokenRequstDto)
        {
            var authUser = new CaptionGeneratorUser();

            var User = await _userManager.FindByEmailAsync(getTokenRequstDto.Email);
            if (User == null || !await _userManager.CheckPasswordAsync(User, getTokenRequstDto.Passward))
            {
                return new CaptionGeneratorUser { Message = "Email or Passward is incorrect" };
            }
            var jwtSecurityToken = await CreateJwtToken(User);
            var rolesList = await _userManager.GetRolesAsync(User);


            authUser.IsAuthenticated = true;
            authUser.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authUser.Email = User.Email;
            authUser.ExpiresOn = jwtSecurityToken.ValidTo;
            authUser.UserName = User.UserName;
            authUser.Roles = rolesList.ToList();

            return authUser;
        }


        public async Task<string> AddRoleAsync(RoleModelDto roleModel)
        {
            var user = await _userManager.FindByIdAsync(roleModel.UserId);

            if (user is null || !await _roleManager.RoleExistsAsync(roleModel.Role))
                return "Invalid user ID or Role";

            if (await _userManager.IsInRoleAsync(user, roleModel.Role))
                return "User already assigned to this role";

            var result = await _userManager.AddToRoleAsync(user, roleModel.Role);

            return result.Succeeded ? string.Empty : "Sonething went wrong";
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays((double)_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<bool> ForgetPasswordAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    // Include the token in the email message
                    var resetPasswordLink = $"{Uri.EscapeDataString(token)}&email={user.Email}";
                    var messageContent = $"Click <a href='{resetPasswordLink}'>here</a> to reset your password. Your token: {token}";

                    // TODO: Replace the following lines with your email sending logic
                    var message = new MailMessage
                    {
                        From = new MailAddress("amroyasser55555@gmail.com", "Tecical Team"),
                        Subject = "Forget Password Link",
                        Body = messageContent,
                        IsBodyHtml = true
                    };

                    // Set the recipient's email address
                    message.To.Add(new MailAddress(user.Email));

                    // Use your SMTP server details
                    using (var smtpClient = new SmtpClient("smtp.gmail.com"))
                    {
                        smtpClient.Port = 587;
                        smtpClient.Credentials = new NetworkCredential("amroyasser55555@gmail.com", "vkko ppmn sihc lupe");
                        smtpClient.EnableSsl = true;

                        // Send the email
                        smtpClient.Send(message);
                    }

                    return true; // Password reset link sent successfully
                }

                return false; // User not found or couldn't send the link to the email
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ForgetPasswordAsync: {ex}");
                throw; // You may handle this exception as needed, e.g., log and return a custom response
            }
        }


        public async Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);

                if (user == null)
                {
                    // User with the provided email does not exist
                    return false;
                }

                var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.ResetToken, resetPasswordDto.NewPassword);

                if (result.Succeeded)
                {
                    // Password reset successful
                    return true;
                }

                // Password reset failed
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ResetPasswordAsync: {ex}");
                throw; // You may handle this exception as needed, e.g., log and return a custom response
            }
        }
    }
}
