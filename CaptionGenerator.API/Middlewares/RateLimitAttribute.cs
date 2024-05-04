using CaptionGenerator.CORE.Dtos;
using CaptionGenerator.CORE.Entities;
using CaptionGenerator.CORE.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CaptionGenerator.API.Middlewares
{
    public class RateLimitAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var keyService = (IkeyService)context.HttpContext.RequestServices.GetService(typeof(IkeyService));
            var userManager = (UserManager<ApplicationUser>)context.HttpContext.RequestServices.GetService(typeof(UserManager<ApplicationUser>));

            var httpContext = context.HttpContext;
            var key = httpContext.Request.Headers["Key"];
            var userKey = await keyService.GetKeyByKeyValueAsync(key);
            if (userKey == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (userKey.Limit != -1 && userKey.Usage >= userKey.Limit)
            {
                context.Result = new StatusCodeResult(429);
                return;
            }

            userKey.Usage++;
            await keyService.UpdateKeyAsync(userKey.Id, new KeyUpdateRequest { Usage = userKey.Usage });

            var applicationUserId = userKey.UserKeys.FirstOrDefault()?.ApplicationUserId;
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == applicationUserId);
            if (user != null && user.Usage < user.Limit)
            {
                user.Usage++;
                await userManager.UpdateAsync(user);
            }

            await next();
        }
    }
}