namespace GestaoEquipamentos.Exceptions;

/// <summary>
/// Represents an operation rejected by an application business rule.
/// </summary>
public sealed class BusinessRuleException : AppException
{
    public BusinessRuleException(string message)
        : base(message)
    {
    }
}
