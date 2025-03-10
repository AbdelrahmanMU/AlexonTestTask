using FluentValidation;

namespace ExperimentsDemo.Application.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Price)
            .NotNull()
            .GreaterThanOrEqualTo(0);
    }
}
