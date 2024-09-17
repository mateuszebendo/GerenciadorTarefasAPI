namespace Domain.Exceptions;

public class TituloDuplicadoException : DomainException
{
    public TituloDuplicadoException(string message) : base(message)
    {
    }
}