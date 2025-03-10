namespace ExperimentsDemo.Application.Queries.SharedDto;

public class ProductDto
{
    public Guid Id { get; init; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public List<CategoryDto> Categories { get; set; } = [];
}
