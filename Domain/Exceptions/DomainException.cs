namespace Domain.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message) {}
}
public class TituloDuplicadoException : DomainException
{
    public TituloDuplicadoException(string message) : base(message) {}
}

public class EmailouSenhaInvalidoException : DomainException
{
    public EmailouSenhaInvalidoException(string message) : base(message) {}
} 