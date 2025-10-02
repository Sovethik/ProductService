using Microsoft.EntityFrameworkCore;
using ProductService.Application.Common.Exceptions;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entity;

namespace ProductService.Infrastructure.RepositorysDb
{
    public class RepositoryProducts : IRepository<Product> 
    {
        private readonly IProductServiceDb _dbContext;
        

        public RepositoryProducts(IProductServiceDb dbContext) 
        {
            _dbContext = dbContext;
            
        }


        public async Task AddAsync(Product entity) => await _dbContext.Products.AddAsync(entity);

        public void Delete(Product entity) => _dbContext.Products.Remove(entity);

        public void Update(Product entity) => _dbContext.Products.Update(entity);

        public async Task<IEnumerable<Product>> GetPage(int numberPage)
        {
            int sizePage = 20;

            if(numberPage <= 0)
                throw new NotFoundException($"list of {nameof(Product)}", $"number page {numberPage}");

            var products = await _dbContext.Products
                    .Include(p => p.Category)
                    .AsNoTracking()
                    .Skip((numberPage - 1) * sizePage)
                    .Take(sizePage)
                    .ToListAsync();

            if (products == null || products.Count == 0)
                throw new NotFoundException($"list of {nameof(Product)}", $"number page {numberPage}");

            return products;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _dbContext.Products.Include(p => p.Category).AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                throw new NotFoundException(nameof(Product), id.ToString());

            return product;
        }

        


    }

}
