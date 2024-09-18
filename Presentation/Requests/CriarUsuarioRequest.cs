using System.ComponentModel.DataAnnotations;

namespace Presentation.Requests;

public class CriarUsuarioRequest
{
    [Required(ErrorMessage = "Nome obrigatório")]
    [MaxLength(255, ErrorMessage = "Máximo de 255 caracteres")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "Email obrigatório")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    public string Email { get; set; }
    [Required(ErrorMessage = "A senha é obrigatória.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]).{8,}$", 
        ErrorMessage = "A senha deve ter no mínimo 8 caracteres, incluindo letra maiúscula, letra minúscula, número e caractere especial.")]
    public string Senha { get; set; }
    
}