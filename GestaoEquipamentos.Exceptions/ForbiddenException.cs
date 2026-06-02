namespace GestaoEquipamentos.Exceptions;

/// <summary>
/// Represents an authenticated user attempting an operation without permission.
/// </summary>
public sealed class ForbiddenException : AppException
{
    public ForbiddenException(string message)
        : base(message)
    {
    }
}
