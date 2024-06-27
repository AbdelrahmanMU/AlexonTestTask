using AlexonTestTask.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AlexonTestTask.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
}
