using MediatR;
using ProductService.Domain.Entity;



namespace ProductService.Application.Products.Querys.QuerysGetById
{
    public class GetByIdProductQuery : IRequest<DetailsProductDto>
    {
        public int Id { get; set; }

    }
}
