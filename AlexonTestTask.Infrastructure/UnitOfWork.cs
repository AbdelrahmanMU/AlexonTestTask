using AlexonTestTask.Core;

namespace AlexonTestTask.Infrastructure;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public void Dispose() => _context.Dispose();
}
