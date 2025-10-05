using MediatR;
using Microsoft.Extensions.Logging;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entity;

namespace ProductService.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductServiceDb _contextDb;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(IProductServiceDb contextDb, ILogger<CreateProductCommandHandler> logger)
        {
            _contextDb = contextDb;
            _logger = logger;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                CategoryId = request.CategoryId
            };

            _contextDb.Products.Add(product);
                          
            await _contextDb.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Was create entity {product.GetType().Name} witch id - {product.Id}");

            return product.Id;

        }
    }
}
