namespace AlexonTestTask.Core;

public interface IUnitOfWork : IDisposable
{
    int Complete();
}
