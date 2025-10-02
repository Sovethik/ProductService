using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProductService.Infrastructure.DataBases;
using ProductService.Application.Interfaces;
using Microsoft.Extensions.Hosting;

namespace ProductService.Infrastructure
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration cfg,
            IHostEnvironment environment)
        {
            if (!environment.IsEnvironment("Test"))
            {
                var connectionString = cfg.GetConnectionString("Default");

                services.AddDbContext<ProductServiceDb>(options =>
                    options.UseNpgsql(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging());
            }
            else
            {
                services.AddDbContext<ProductServiceDb>(options =>
                       options.UseInMemoryDatabase("TestDb"));
            }

            services.AddScoped<IProductServiceDb, ProductServiceDb>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
