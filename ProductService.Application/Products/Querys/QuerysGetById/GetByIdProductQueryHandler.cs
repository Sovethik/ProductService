using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductService.Application.Common.Exceptions;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entity;

namespace ProductService.Application.Products.Querys.QuerysGetById
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, DetailsProductDto>
    {
        private readonly IProductServiceDb _contextDb;
        private readonly IMapper _mapper;

        public GetByIdProductQueryHandler(IProductServiceDb contextDb, IMapper mapper) 
        {
            _contextDb = contextDb;
            _mapper = mapper;
        }


        public async Task<DetailsProductDto> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _contextDb.Products.Include(p => p.Category).AsNoTracking().FirstOrDefaultAsync(p => p.Id == request.Id);

            if (product == null)
                throw new NotFoundException(nameof(Product), request.Id.ToString());

            var dto = _mapper.Map<DetailsProductDto>(product);

            return dto;
        }
    }
}
