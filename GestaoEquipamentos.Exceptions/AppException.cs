namespace GestaoEquipamentos.Exceptions;

/// <summary>
/// Base type for errors that are expected and can be presented to API clients.
/// </summary>
public abstract class AppException : Exception
{
    protected AppException(string message)
        : base(message)
    {
    }

    protected AppException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
