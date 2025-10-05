using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductService.Application.Common.Exceptions;
using ProductService.Application.Interfaces;
using ProductService.Application.Products.Commands.CreateProduct;
using ProductService.Domain.Entity;

namespace ProductService.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductServiceDb _contextDb;
        private readonly ILogger<DeleteProductCommandHandler> _logger;

        public DeleteProductCommandHandler(IProductServiceDb contextDb, ILogger<DeleteProductCommandHandler> logger)
        {
            _contextDb = contextDb;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var deleteProduct = await _contextDb.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == request.Id);

            if (deleteProduct == null)
                throw new NotFoundException(nameof(Product), request.Id.ToString());

            _contextDb.Products.Remove(deleteProduct);

            await _contextDb.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Was delete entity {deleteProduct.GetType().Name} witch id - {deleteProduct.Id}");

            return Unit.Value;
        }
    }
}
