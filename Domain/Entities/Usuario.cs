using Domain.Enums;

namespace Domain.Entities;

public class Usuario
{
    public int UsuarioId { get; set; }
    public string Nome { get; set; }
    public UsuarioStatus Status { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }

    public Usuario(int usuarioId, string nome, UsuarioStatus status, string email, string senha)
    {
        UsuarioId = usuarioId;
        Nome = nome;
        Status = status;
        Email = email;
        Senha = senha;
    }

    public Usuario(string nome, UsuarioStatus status, string email, string senha)
    {
        Nome = nome;
        Status = status;
        Email = email;
        Senha = senha;
    }
}