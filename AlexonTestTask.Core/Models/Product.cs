namespace AlexonTestTask.Core.Models;

public class Product
{
    public Product()
    {
        Id = Guid.NewGuid();    
    }

    public Guid Id { get; init; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public ICollection<Category> Categories { get; set; } = [];
}
