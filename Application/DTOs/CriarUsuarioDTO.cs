using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs;

public class CriarUsuarioDTO
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    
    public UsuarioStatus Status { get; set; }

    public CriarUsuarioDTO(string nome, string email, string senha)
    {
        Nome = nome;
        Email = email;
        Senha = senha;
        Status = UsuarioStatus.Ativo;
    }
}