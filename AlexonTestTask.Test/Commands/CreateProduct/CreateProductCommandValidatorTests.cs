using AlexonTestTask.Application.Commands.CreateProduct;
using FluentValidation.TestHelper;

namespace AlexonTestTask.Test.Commands.CreateProduct;

public class CreateProductCommandValidatorTests
{
    private readonly CreateProductCommandValidator _validator;

    public CreateProductCommandValidatorTests()
    {
        _validator = new CreateProductCommandValidator();
    }

    [Fact]
    public void Validate_NameIsNull_ShouldHaveValidationError()
    {
        var command = new CreateProductCommand { Name = null!, Price = 10m };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validate_NameIsEmpty_ShouldHaveValidationError()
    {
        var command = new CreateProductCommand { Name = string.Empty, Price = 10m };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validate_NameIsValid_ShouldNotHaveValidationError()
    {
        var command = new CreateProductCommand { Name = "Valid Name", Price = 10m };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validate_PriceIsNegative_ShouldHaveValidationError()
    {
        var command = new CreateProductCommand { Name = "Valid Name", Price = -1m };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Price);
    }

    [Fact]
    public void Validate_PriceIsValid_ShouldNotHaveValidationError()
    {
        var command = new CreateProductCommand { Name = "Valid Name", Price = 10m };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(x => x.Price);
    }
}
