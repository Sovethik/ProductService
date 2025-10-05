using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductService.Application.Common.Exceptions;
using ProductService.Application.Interfaces;
using ProductService.Application.Products.Commands.DeleteProduct;
using ProductService.Domain.Entity;

namespace ProductService.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IProductServiceDb _contextDb;
        private readonly ILogger<UpdateProductCommandHandler> _logger;


        public UpdateProductCommandHandler(IProductServiceDb contextDb, ILogger<UpdateProductCommandHandler> logger)
        {
            _contextDb = contextDb;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {

            var isExistProduct = await _contextDb.Products.AnyAsync(p => p.Id == request.Id);

            if (!isExistProduct)
                throw new NotFoundException(nameof(Product), request.Id.ToString());

            Product product = new Product()
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId
            };

           
             _contextDb.Products.Update(product);
            
           

            await _contextDb.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Was update entity {product.GetType().Name} witch id - {product.Id}");

            return Unit.Value;
        }
    }
}
