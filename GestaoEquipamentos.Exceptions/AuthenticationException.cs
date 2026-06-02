namespace GestaoEquipamentos.Exceptions;

/// <summary>
/// Represents a failure to authenticate the current request.
/// </summary>
public sealed class AuthenticationException : AppException
{
    public AuthenticationException(string message)
        : base(message)
    {
    }
}
