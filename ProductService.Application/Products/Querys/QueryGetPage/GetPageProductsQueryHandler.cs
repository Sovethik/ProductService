using AutoMapper;
using MediatR;
using ProductService.Application.Interfaces;
using ProductService.Application.Products.Querys.QueryGetAll;
using ProductService.Domain.Entity;

namespace ProductService.Application.Products.Querys.QueryGetPage
{

    public class GetPageProductsQueryHandler : IRequestHandler<GetPageProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPageProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ProductDto>> Handle(GetPageProductsQuery request, CancellationToken cancellationToken)
        {
           var pageProducts = await _unitOfWork.Products.GetPage(request.NumberPage);

            var dto = _mapper.Map<IEnumerable<ProductDto>>(pageProducts);

            return dto;
        }
    }
}
