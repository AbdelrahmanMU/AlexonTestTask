namespace ExperimentsDemo.API.Exceptions;

public sealed class ValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary) : ApplicationException()
{
    public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; } = errorsDictionary;
}