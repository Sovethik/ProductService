using Moq;
using ProductService.Application.Interfaces;
using ProductService.Application.Products.Commands.CreateProduct;
using ProductService.Domain.Entity;


namespace ProductService.Tests.UnitTest.TestCommand
{
    public class UnitTestCreateCommand
    {
        [Fact]
        public async Task Test_CreateCommandHandler_WhenProductExist()
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();

            var handlerCommand = new CreateProductCommandHandler(unitOfWork.Object);

            var command = new CreateProductCommand()
            {
                Name = "Клавиатура",
                Price = 3000,
                Description = "Механическая",
                CategoryId = 1
            };

            CancellationToken cancellationToken = CancellationToken.None;

            unitOfWork.Setup(u => u.Products.AddAsync(It.IsAny<Product>()))
                .Callback<Product>(product => product.Id = 1)
                .Returns(Task.CompletedTask);
            unitOfWork.Setup(u => u.SaveChangesAsync(cancellationToken))
                .ReturnsAsync(1);
                
            //Act
            var result = await handlerCommand.Handle(command, cancellationToken);


            //Assert
            Assert.Equal(1, result);
        }
    }
}
