namespace Application.DTOs;

public class LoginUsuarioDTO
{
    public string Email { get; set; }
    public string SenhaInserida { get; set; }

    public LoginUsuarioDTO(string email, string senhaInserida)
    {
        Email = email;
        SenhaInserida = senhaInserida;
    }
}