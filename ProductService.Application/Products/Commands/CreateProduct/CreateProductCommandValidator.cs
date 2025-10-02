using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProductService.Application.Interfaces;

namespace ProductService.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IProductServiceDb _contextDb;

        public CreateProductCommandValidator(IProductServiceDb contextDb)
        {
            _contextDb = contextDb;

            RuleFor(p => p.Name).NotEmpty().MaximumLength(100);

            RuleFor(p => p.Description).NotEmpty().MaximumLength(300);

            RuleFor(p => p.Price).GreaterThanOrEqualTo(0);

            RuleFor(p => p.CategoryId).Must(existCategory).WithMessage("Not found category");
        }

        private bool existCategory(int categoryId)
        {
            return _contextDb.Categories.Any(c => c.Id == categoryId);
        }
    }
}
