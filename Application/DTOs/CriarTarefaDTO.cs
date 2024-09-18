using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs;

public class CriarTarefaDTO
{
    [Required(ErrorMessage = "Título obrigatório")]
    [MaxLength(100, ErrorMessage = "O título deve conter no máximo 100 caracteres.")]
    public string Titulo { get; set; }
    
    [MaxLength(1024, ErrorMessage = "A descrição deve conter no máximo 1024 caracteres.")]
    public string Descricao { get; set; }
    
    public DateTime DataCriacao { get; set; }
    
    public Status Status { get; set; }
    
    public int UsuarioId { get; set; }

    public CriarTarefaDTO(string titulo, string descricao, int usuarioId)
    {
        Titulo = titulo;
        Descricao = descricao;
        DataCriacao = DateTime.Today;
        Status = Status.Pendente;
        UsuarioId = usuarioId;
    }
}