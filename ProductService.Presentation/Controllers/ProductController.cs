using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Products.Commands.CreateProduct;
using ProductService.Application.Products.Querys.QueryGetAll;
using ProductService.Application.Products.Querys.QueryGetPage;
using ProductService.Application.Products.Querys.QuerysGetById;
using ProductService.Domain.Entity;

namespace ProductService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetPageProductsAsync(int numberPage)
        {
            GetPageProductsQuery query = new GetPageProductsQuery() { NumberPage = numberPage };
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailsProductDto>> GetByIdProduct(int id)
        {
            GetByIdProductQuery query = new GetByIdProductQuery() { Id = id };
            var result = await _mediator.Send(query);
            
            return Ok(result);
        }

        [HttpPost(Name = "CreateProduct")]
        public async Task<ActionResult<int>> CreateProductAsync(CreateProductCommand product)
        {
            var result = await _mediator.Send(product);

            return Ok(result);
        }
    }
}
