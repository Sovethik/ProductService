

using FluentValidation;

namespace ProductService.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(c => c.Id).GreaterThanOrEqualTo(1);
        }
    }
}
