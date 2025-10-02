using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Domain.Entity;
using ProductService.Infrastructure.DataBases;
using System.Net.Http.Json;


namespace ProductService.Tests.IntegrationTest
{
    public class EnvironmentTestHttpClient : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly HttpClient _client;

        public EnvironmentTestHttpClient(WebApplicationFactory<Program> factory)
        {
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Test");

                builder.ConfigureServices(
                    services =>
                    {
                        var sp = services.BuildServiceProvider();
                        using var scope = sp.CreateScope();
                        var db = scope.ServiceProvider.GetRequiredService<ProductServiceDb>();

                        db.Database.EnsureDeleted();
                        db.Database.EnsureCreated();

                        
                        db.Categories.Add(TestDataForDataBase.GetCategory());
                        db.Products.Add(TestDataForDataBase.GetProduct());


                        db.SaveChanges();

                        var product = db.Products.FirstOrDefault();
                    });
                

            }).CreateClient();
        }

        
    }
}
