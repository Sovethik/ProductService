using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ProductService.Application.Interfaces;
using ProductService.Application.Products.Commands.CreateProduct;
using ProductService.Domain.Entity;
using ProductService.Tests.UnitTest.Common;


namespace ProductService.Tests.UnitTest.TestCommand
{
    public class UnitTestCreateCommand : TestHandlerBase<CreateProductCommandHandler>
    {
        [Fact]
        public async Task CreateCommandHandler_WhenProductExist_ReturnId()
        {
            await AddTestDataInDataBaseAndReturnData();

            //Arrange
            var command = new CreateProductCommand()
            {
                Name = "Клавиатура",
                Price = 3000,
                Description = "Механическая",
                CategoryId = 1
            };

            var handlerCommand = new CreateProductCommandHandler(contextDb, mockLogger.Object);

            //Act
            var result = await handlerCommand.Handle(command, сancellationToken);

            var createdProduct = await contextDb.Products.FirstOrDefaultAsync(x =>
            x.Id == result
            && x.Name == command.Name
            && x.Description == command.Description
            && x.CategoryId == command.CategoryId);

            //Assert
            Assert.Equal(createdProduct.Id, result);
            Assert.NotNull(createdProduct);

        }
    }
}
