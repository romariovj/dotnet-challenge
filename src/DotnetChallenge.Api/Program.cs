
using DotnetChallenge.Api.Middlewares;
using DotnetChallenge.Application;
using DotnetChallenge.Infrastructure;
using Microsoft.OpenApi.Models;

namespace DotnetChallenge.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DotnetChallenge API",
                    Version = "v1",
                    Description = "Dotnet Challenge of Tekton",
                    Contact = new OpenApiContact
                    {
                        Name = "Romario Vargas",
                        Email = "romariovargasj@gmail.com"
                    },
                });
            });



            var app = builder.Build();

            app.UseMiddleware<ResponseTimeLoggingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
