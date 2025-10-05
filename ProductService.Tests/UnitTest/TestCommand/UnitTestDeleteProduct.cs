using Microsoft.EntityFrameworkCore;
using ProductService.Application.Products.Commands.DeleteProduct;
using ProductService.Domain.Entity;
using ProductService.Tests.UnitTest.Common;


namespace ProductService.Tests.UnitTest.TestCommand
{
    public class UnitTestDeleteProduct : TestHandlerBase<DeleteProductCommandHandler>
    {
        [Fact]
        public async Task DeleteCommandHandler_WhenProductExist()
        {
            //Arrange
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

            contextDb.Products.Add(product);
            await contextDb.SaveChangesAsync();

            var command = new DeleteProductCommand() { Id = 1 };

            var handlerCommand = new DeleteProductCommandHandler(contextDb, mockLogger.Object);


            //Act
            await handlerCommand.Handle(command, сancellationToken);
            var deletedProduct = contextDb.Products.AsNoTracking().FirstOrDefault(p => p.Id == product.Id);

            //Assert
            Assert.Null(deletedProduct);

        }
    }
}
