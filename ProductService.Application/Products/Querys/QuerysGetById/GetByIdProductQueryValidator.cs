using FluentValidation;


namespace ProductService.Application.Products.Querys.QuerysGetById
{
    public class GetByIdProductQueryValidator : AbstractValidator<GetByIdProductQuery>
    {
        public GetByIdProductQueryValidator()
        {
            RuleFor(q => q.Id).GreaterThanOrEqualTo(1);
        }
    }
}
