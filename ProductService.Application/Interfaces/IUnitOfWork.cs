using ProductService.Domain.Entity;

namespace ProductService.Application.Interfaces
{
    public interface IUnitOfWork
    {
       IRepository<Product> Products { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
