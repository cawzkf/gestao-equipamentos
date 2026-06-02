namespace GestaoEquipamentos.Exceptions;

/// <summary>
/// Represents one or more input validation failures.
/// </summary>
public sealed class ValidationException : AppException
{
    public ValidationException(string message)
        : this(message, new Dictionary<string, string[]>())
    {
    }

    public ValidationException(IReadOnlyDictionary<string, string[]> errors)
        : this("Um ou mais erros de validação ocorreram.", errors)
    {
    }

    public ValidationException(
        string message,
        IReadOnlyDictionary<string, string[]> errors)
        : base(message)
    {
        Errors = errors;
    }

    public IReadOnlyDictionary<string, string[]> Errors { get; }
}
