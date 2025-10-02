using MediatR;
using ProductService.Application.Products.Querys.QueryGetPage;
using ProductService.Domain.Entity;


namespace ProductService.Application.Products.Querys.QueryGetAll
{
    public class GetPageProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
        public int NumberPage { get; set; }
    }
}
