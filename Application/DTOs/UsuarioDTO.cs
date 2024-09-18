using Domain.Enums;

namespace Application.DTOs;

public class UsuarioDTO
{
    public int UsuarioId { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    
    public UsuarioStatus Status { get; set; }

    public UsuarioDTO(int usuarioId, string nome, string email, string senha, UsuarioStatus status)
    {
        UsuarioId = usuarioId;
        Nome = nome;
        Email = email;
        Senha = senha;
        Status = status;
    }

    public UsuarioDTO(CriarUsuarioDTO criarUsuarioDto)
    {
        Nome = criarUsuarioDto.Nome;
        Email = criarUsuarioDto.Email;
        Senha = criarUsuarioDto.Senha;
        Status = criarUsuarioDto.Status;
    }
}