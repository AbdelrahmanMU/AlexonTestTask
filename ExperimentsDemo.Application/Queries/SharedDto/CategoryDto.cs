namespace ExperimentsDemo.Application.Queries.SharedDto;

public class CategoryDto
{
    public Guid Id { get; init; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}