namespace GestaoEquipamentos.Exceptions;

/// <summary>
/// Represents an attempt to access a resource that does not exist.
/// </summary>
public sealed class NotFoundException : AppException
{
    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(string resourceName, object resourceId)
        : base($"{resourceName} com identificador '{resourceId}' não foi encontrado.")
    {
    }
}
