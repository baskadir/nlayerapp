using FluentValidation;
using NLayerApp.Core.DTOs.Categories;

namespace NLayerApp.Service.Validations;

public class CategoryAddDtoValidator : AbstractValidator<CategoryAddDto>
{
	public CategoryAddDtoValidator()
	{
        RuleFor(p => p.Name).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
    }
}
