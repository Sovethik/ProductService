using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Domain.Entity;
using ProductService.Infrastructure.DataBases;
using System;
using System.Net.Http.Json;

namespace ProductService.Tests.IntegrationTest.Common
{
    public class EnvironmentTestHttpClient : IClassFixture<WebApplicationFactory<Program>>
    {
        protected HttpClient _client;

        public EnvironmentTestHttpClient(WebApplicationFactory<Program> factory)
        {
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<ProductServiceDb>));
                    if (descriptor != null)
                        services.Remove(descriptor);

                    services.AddDbContext<ProductServiceDb>(options =>
                    {
                        options.UseInMemoryDatabase("TestDb-" + GetType().Name);
                    });

                    using var serviceProvider = services.BuildServiceProvider();
                    using var scope = serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<ProductServiceDb>();

                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    context.Categories.Add(TestDataForDataBase.GetCategory());
                    context.Products.Add(TestDataForDataBase.GetProduct());

                    context.SaveChanges();

                });
            }).CreateClient();

            

        }

        
    }
}
