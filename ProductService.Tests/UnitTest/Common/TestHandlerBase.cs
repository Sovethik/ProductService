using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using ProductService.Domain.Entity;
using ProductService.Infrastructure.DataBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Tests.UnitTest.Common
{
    public class TestHandlerBase<TLogger> /*: IDisposable*/
        where TLogger : class
    {
        protected readonly ProductServiceDb contextDb;
        protected readonly Mock<ILogger<TLogger>> mockLogger;
        protected readonly CancellationToken сancellationToken;


        public TestHandlerBase()
        {
            mockLogger = new Mock<ILogger<TLogger>>();
            contextDb = ProductServiceContextFactory.Create();
            сancellationToken = CancellationToken.None;
        }

        public async Task<Product> AddTestDataInDataBaseAndReturnData()
        {
            Category category = new Category()
            {
                Id = 1,
                TypeCategory = "TestCategory"
            };

            contextDb.Categories.Add(category);
            await contextDb.SaveChangesAsync();

            Product product = new Product()
            {
                Name = "TestName",
                Price = 1000,
                Description = "TestDescription",
                CategoryId = 1
            };
            await contextDb.SaveChangesAsync();

            return product;
        }

        //public void Dispose()
        //{
        //    contextDb.Database.EnsureDeleted();
        //    contextDb.Dispose();
        //}
    }
}
