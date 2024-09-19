using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs;

public class AtualizarTarefaDTO
{
    [Required(ErrorMessage = "Título obrigatório")]
    [MaxLength(100, ErrorMessage = "O título deve conter no máximo 100 caracteres.")]
    public string Titulo { get; set; }
    
    [MaxLength(1024, ErrorMessage = "A descrição deve conter no máximo 1024 caracteres.")]
    public string Descricao { get; set; }
    
    
    public string UsuarioId { get; set; }

    public AtualizarTarefaDTO(string titulo, string descricao, string usuarioId)
    {
        Titulo = titulo;
        Descricao = descricao;
        UsuarioId = usuarioId;
    }
}