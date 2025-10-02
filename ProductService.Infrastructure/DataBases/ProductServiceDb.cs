using Microsoft.EntityFrameworkCore;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entity;
using ProductService.Infrastructure.DataBases.EntityConfiguration;

namespace ProductService.Infrastructure.DataBases
{
    public class ProductServiceDb : DbContext, IProductServiceDb
    {
        public ProductServiceDb(DbContextOptions<ProductServiceDb> options) : base(options)
        {}

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            base.OnModelCreating(builder);
        }
        
    }
}
