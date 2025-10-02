using AutoMapper;
using MediatR;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entity;

namespace ProductService.Application.Products.Querys.QuerysGetById
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, DetailsProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByIdProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<DetailsProductDto> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.Id);
            var dto = _mapper.Map<DetailsProductDto>(product);

            return dto;
        }
    }
}
