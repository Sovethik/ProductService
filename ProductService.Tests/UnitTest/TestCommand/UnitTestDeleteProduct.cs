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
            await AddTestDataInDataBaseAndReturnData();

            var command = new DeleteProductCommand() { Id = 1 };

            var handlerCommand = new DeleteProductCommandHandler(contextDb, mockLogger.Object);

            //Act
            await handlerCommand.Handle(command, сancellationToken);
            var deletedProduct = contextDb.Products.AsNoTracking().FirstOrDefault(p => p.Id == command.Id);

            //Assert
            Assert.Null(deletedProduct);

        }
    }
}
