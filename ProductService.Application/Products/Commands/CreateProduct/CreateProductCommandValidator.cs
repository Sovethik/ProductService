using FluentValidation;
using ProductService.Application.Interfaces;

namespace ProductService.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {

        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(p => p.Description).NotNull().NotEmpty().MaximumLength(300);
            RuleFor(p => p.Price).GreaterThanOrEqualTo(0);
            RuleFor(p => p.CategoryId).GreaterThanOrEqualTo(1);
        }
    }
}
