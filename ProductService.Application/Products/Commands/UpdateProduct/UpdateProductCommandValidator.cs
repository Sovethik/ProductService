using FluentValidation;

namespace ProductService.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(c => c.Id).GreaterThanOrEqualTo(1);
            RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(c => c.Description).NotNull().NotEmpty().MaximumLength(300);
            RuleFor(c => c.Price).GreaterThanOrEqualTo(0);
            RuleFor(c => c.CategoryId).GreaterThanOrEqualTo(1);
        }
    }
}
