using System.ComponentModel.DataAnnotations;

namespace Presentation.Requests;

public class CriarTarefaRequest
{
    [Required(ErrorMessage = "Título obrigatório")]
    [MaxLength(100, ErrorMessage = "O título deve conter no máximo 100 caracteres.")]
    public string Titulo { get; set; }
    
    [MaxLength(1024, ErrorMessage = "A descrição deve conter no máximo 1024 caracteres.")]
    public string Descricao { get; set; }
    
    [Required(ErrorMessage = "Usuário necessário")]
    public int UsuarioId { get; set; }
}