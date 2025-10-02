using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entity;

namespace ProductService.Application.Interfaces
{
    public interface IProductServiceDb
    {
        DbSet<Product> Products { get; set; }
        DbSet<Category> Categories { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
