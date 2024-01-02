using DotnetChallenge.Domain.Repositories;
using DotnetChallenge.Infrastructure.Contexts;
using DotnetChallenge.Infrastructure.Mappings;
using DotnetChallenge.Infrastructure.Persistences;
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

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            );

            return services;
        }
    }
}
