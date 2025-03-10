namespace ExperimentsDemo.Core;

public interface IUnitOfWork : IDisposable
{
    int Complete();
}
