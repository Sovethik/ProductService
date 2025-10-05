using Microsoft.EntityFrameworkCore;
using ProductService.Infrastructure.DataBases;

namespace ProductService.Tests.UnitTest.Common
{
    public class ProductServiceContextFactory
    {
        public static ProductServiceDb Create()
        {
            var options = new DbContextOptionsBuilder<ProductServiceDb>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ProductServiceDb(options);

            context.Database.EnsureCreated();

            return context;
        }

    }
}
