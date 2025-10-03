using Microsoft.EntityFrameworkCore;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entity;
using ProductService.Infrastructure.RepositorysDb;

namespace ProductService.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Product> Products { get; }
        private readonly IProductServiceDb _contextDb;

        public UnitOfWork(IProductServiceDb contextDb)
        {
            _contextDb = contextDb;
            Products = new RepositoryProducts(contextDb);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            int countSaveRows = 0;
            try
            {
                countSaveRows = await _contextDb.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {

            }

            return countSaveRows;
        }
    }
}
