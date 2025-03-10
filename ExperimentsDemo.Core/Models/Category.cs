namespace ExperimentsDemo.Core.Models;

public class Category
{
    public Category()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; init; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public ICollection<Product> Products { get; set; } = [];
}
