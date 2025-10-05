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
    public class TestHandlerBase<TLogger> : IDisposable
        where TLogger : class
    {
        protected readonly ProductServiceDb contextDb;
        protected readonly Mock<ILogger<TLogger>> mockLogger;
        protected readonly CancellationToken сancellationToken;
        private string _nameDataBase;

        public TestHandlerBase()
        {
            _nameDataBase = GetType().Name;
            mockLogger = new Mock<ILogger<TLogger>>();
            contextDb = ProductServiceContextFactory.Create(_nameDataBase);
            сancellationToken = CancellationToken.None;
        }

        public async Task AddTestDataInDataBaseAndReturnData()
        {

            var contextToSaveData = ProductServiceContextFactory.Create(_nameDataBase);

            Category category = new Category()
            {
                TypeCategory = "TestCategory"
            };

            contextToSaveData.Categories.Add(category);

            Product product = new Product()
            {
                Name = "TestName",
                Price = 1000,
                Description = "TestDescription",
                CategoryId = 1
            };

            contextToSaveData.Products.Add(product);

        
            await contextToSaveData.SaveChangesAsync();

        }

        public void Dispose()
        {
            contextDb.Database.EnsureDeleted();
            contextDb.Dispose();
        }
    }
}
