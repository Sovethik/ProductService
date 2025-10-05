using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductService.Application.Common.Exceptions;
using ProductService.Application.Interfaces;
using ProductService.Application.Products.Querys.QueryGetAll;
using ProductService.Domain.Entity;

namespace ProductService.Application.Products.Querys.QueryGetPage
{

    public class GetPageProductsQueryHandler : IRequestHandler<GetPageProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductServiceDb _contextDb;
        private readonly IMapper _mapper;

        public GetPageProductsQueryHandler(IProductServiceDb contextDb, IMapper mapper)
        {
            _contextDb = contextDb;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ProductDto>> Handle(GetPageProductsQuery request, CancellationToken cancellationToken)
        {

            var sizePage = 20;
            var numberPage = request.NumberPage;

            if (numberPage <= 0)
                throw new NotFoundException($"list of {nameof(Product)}", $"number page {numberPage}");

            var pageProducts = await _contextDb.Products
                    .Include(p => p.Category)
                    .AsNoTracking()
                    .OrderBy(p => p.Id)
                    .Skip((numberPage - 1) * sizePage)
                    .Take(sizePage)
                    .ToListAsync();

            if (pageProducts == null || pageProducts.Count == 0)
                throw new NotFoundException($"list of {nameof(Product)}", $"number page {numberPage}");

            var dto = _mapper.Map<IEnumerable<ProductDto>>(pageProducts);

            return dto;
        }
    }
}
