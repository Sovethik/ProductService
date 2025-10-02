using AutoMapper;
using ProductService.Application.Products.Querys.QueryGetPage;
using ProductService.Application.Products.Querys.QuerysGetById;
using ProductService.Domain.Entity;

namespace ProductService.Application.Common.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Category,
                    opt => opt.MapFrom(src => src.Category.TypeCategory));

            CreateMap<Product, DetailsProductDto>()
                .ForMember(dest => dest.Category,
                    opt => opt.MapFrom(src => src.Category.TypeCategory));
        }
    }
}
