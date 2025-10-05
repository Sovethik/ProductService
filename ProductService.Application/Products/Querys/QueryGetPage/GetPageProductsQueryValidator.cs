

using FluentValidation;
using ProductService.Application.Products.Querys.QueryGetAll;

namespace ProductService.Application.Products.Querys.QueryGetPage
{
    public class GetPageProductsQueryValidator : AbstractValidator<GetPageProductsQuery>
    {
        public GetPageProductsQueryValidator() 
        {
            RuleFor(q => q.NumberPage).GreaterThanOrEqualTo(1);
        }
    }
}
