using FluentValidation;
using ProductService.Application.Products.Commands;

namespace ProductService.Application.Products.Validators;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(255)
            .WithMessage("Name can't be empty or exceed 255 characters.");

        RuleFor(p => p.Price)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0)
            .WithMessage("Price can't be empty, 0 or below.");
        
        RuleFor(p => p.Stock)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stock can't be below 0.");
    }
}