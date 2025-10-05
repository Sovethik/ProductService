using Microsoft.EntityFrameworkCore;
using ProductService.Application.Products.Commands.UpdateProduct;
using ProductService.Domain.Entity;
using ProductService.Tests.UnitTest.Common;
using System.Threading;

namespace ProductService.Tests.UnitTest.TestCommand
{
    public class UnitTestUpdateProduct : TestHandlerBase<UpdateProductCommandHandler>
    {
        [Fact]
        public async Task UpdateCommandHandler_WhenProductExist()
        {
            //Arrange
            var product = await AddTestDataInDataBaseAndReturnData();

            var command = new UpdateProductCommand()
            {
                Id = product.Id,
                Name = "TestNewName",
                Description = "TestNewDescription",
                Price = 5000,
                CategoryId = product.CategoryId
            };


            var handlerCommand = new UpdateProductCommandHandler(contextDb, mockLogger.Object);
            //Act
            await handlerCommand.Handle(command, сancellationToken);



            var updatedProduct = await contextDb.Products.FirstOrDefaultAsync(p => 
            p.Id == command.Id
            && p.Name == command.Name
            && p.Description == command.Description
            && p.Price == command.Price
            && p.CategoryId == command.CategoryId);


            //Assert
            Assert.NotNull(updatedProduct);
        }
    }
}
