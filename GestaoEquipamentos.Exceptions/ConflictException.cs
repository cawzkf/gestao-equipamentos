namespace GestaoEquipamentos.Exceptions;

/// <summary>
/// Represents an operation that conflicts with the current application state.
/// </summary>
public sealed class ConflictException : AppException
{
    public ConflictException(string message)
        : base(message)
    {
    }

    public ConflictException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
