using DotnetChallenge.Domain.Interfaces;
using DotnetChallenge.Domain.Repositories;
using DotnetChallenge.Infrastructure.ExternalServices;
using DotnetChallenge.Infrastructure.Mappings;
using DotnetChallenge.Infrastructure.Persistences;
using DotnetChallenge.Infrastructure.Persistences.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetChallenge.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddHttpClient<IDiscountService, HttpDiscountService>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            );

            return services;
        }
    }
}
