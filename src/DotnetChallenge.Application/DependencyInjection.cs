using DotnetChallenge.Application.Mappings;
using DotnetChallenge.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DotnetChallenge.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductStatusCacheService, ProductStatusCacheService>();
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddMemoryCache();
            

            return services;
        }
    }
}
