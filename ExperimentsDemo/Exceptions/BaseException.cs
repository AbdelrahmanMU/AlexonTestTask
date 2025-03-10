namespace ExperimentsDemo.API.Exceptions;

public class BaseException : Exception
{
    public int StatusCode { get; protected set; } = StatusCodes.Status500InternalServerError;

    /// <summary>
    /// Throw bad request exception
    /// </summary>
    public BaseException()
        : base()
    {
    }

    /// <summary>
    /// Throw bad request exception
    /// </summary>
    public BaseException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Throw bad request exception
    /// </summary>
    public BaseException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public BaseException(string message, int statusCode)
        : base(message)
    {
        StatusCode = statusCode;
    }

    public BaseException(string message, Exception innerException, int statusCode)
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }

    /// <summary>
    /// Throw not found exception
    /// </summary>
    /// <param name="name"></param>
    /// <param name="key"></param>
    public BaseException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }
}