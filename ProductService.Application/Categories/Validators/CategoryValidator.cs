using FluentValidation;
using ProductService.Domain.Models;

namespace ProductService.Application.Categories.Validators;

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(255)
            .WithMessage("Name can't be empty or exceed 255 characters.");
    }
}