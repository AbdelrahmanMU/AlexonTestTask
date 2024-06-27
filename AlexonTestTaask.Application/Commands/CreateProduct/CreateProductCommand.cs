using MediatR;

namespace AlexonTestTask.Application.Commands.CreateProduct;

public class CreateProductCommand: IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public List<Guid> CategoriesIds { get; set; } = [];
}
