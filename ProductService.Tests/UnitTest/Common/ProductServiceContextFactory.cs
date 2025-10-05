using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entity;
using ProductService.Infrastructure.DataBases;

namespace ProductService.Tests.UnitTest.Common
{
    public class ProductServiceContextFactory
    {
        public static ProductServiceDb Create(string nameDataBase)
        {
            var options = new DbContextOptionsBuilder<ProductServiceDb>()
                .UseInMemoryDatabase(nameDataBase)
                .EnableSensitiveDataLogging()
                .Options;

            var context = new ProductServiceDb(options);

            context.Database.EnsureCreated();

            return context;
        }

    }
}
