using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Entities;
using CaptionGenerator.CORE.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CaptionGenerator.API.Middlewares
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IkeyService _keyService;
        private readonly UserManager<ApplicationUser> _userManager;

        public RateLimitingMiddleware(RequestDelegate next, IkeyService keyService , UserManager<ApplicationUser> userManager)
        {
            _next = next;
            _keyService = keyService;
            _userManager = userManager;
        }

        public async Task Invoke(HttpContext context)
        {
            var key = context.Request.Headers["Key"];
            var userKey = await _keyService.GetKeyByKeyValueAsync(key);
            if (userKey == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid key");
                return;
            }

            if (userKey.RateLimit != -1 && userKey.Usage >= userKey.RateLimit)
            {
                context.Response.StatusCode = 429;
                await context.Response.WriteAsync("Rate limit exceeded");
                return;
            }

            userKey.Usage++;
            await _keyService.UpdateKeyAsync(userKey.Id, new KeyUpdateRequest { Usage = userKey.Usage });

            // Get ApplicationUser ID
            var applicationUserId = userKey.UserKeys.FirstOrDefault()?.ApplicationUserId;

            // Update ApplicationUser usage
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == applicationUserId);
            if (user != null && user.Usage < user.Limit)
            {
                user.Usage++;
                await _userManager.UpdateAsync(user);
            }

            await _next(context);
        }

    }

}
