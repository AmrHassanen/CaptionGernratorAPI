using CaptionGenerator.CORE.Interfaces;
using CaptionGenerator.EF.Repositories;
using CaptionGenerator.EF.Services;
using Microsoft.Extensions.DependencyInjection;
using Rootics.CORE.Interfaces;
using Rootics.EF.Repositories;

namespace CaptionGenerator.API.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthUser, AuthUser>();
            services.AddScoped<IServiceUser, ServiceUser>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IEndpointService, EndpointService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IkeyService, KeyService>();

        }
    }
}
