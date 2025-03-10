using MediatR;
using System.Text.Json.Serialization;

namespace ExperimentsDemo.Application.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<Unit>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public List<Guid> CategoriesIds { get; set; } = [];
}
