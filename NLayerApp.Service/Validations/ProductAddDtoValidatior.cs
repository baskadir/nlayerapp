using FluentValidation;
using NLayerApp.Core.DTOs.Products;

namespace NLayerApp.Service.Validations;

public class ProductAddDtoValidatior : AbstractValidator<ProductAddDto>
{
	public ProductAddDtoValidatior()
	{
		RuleFor(p => p.Name).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
		RuleFor(p => p.Price).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater than 0");
        RuleFor(p => p.Stock).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater than 0");
        RuleFor(p => p.CategoryId).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater than 0");
    }
}
