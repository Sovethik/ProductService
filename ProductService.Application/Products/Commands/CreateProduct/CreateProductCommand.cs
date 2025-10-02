using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ProductService.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<int>
    {

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }
    }
}
