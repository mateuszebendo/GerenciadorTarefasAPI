using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs;

public class TarefaDTO
{
    public int TarefaId { get; set; }
    
    [Required(ErrorMessage = "Título obrigatório")]
    [MaxLength(100, ErrorMessage = "O título deve conter no máximo 100 caracteres.")]
    public string Titulo { get; set; }
    
    [MaxLength(1024, ErrorMessage = "A descrição deve conter no máximo 1024 caracteres.")]
    public string Descricao { get; set; }
    
    public DateTime DataCriacao { get; set; }
    
    public DateTime DataConclusao { get; set; }
    
    public Status Status { get; set; }

    public TarefaDTO(int tarefaId, string titulo, string descricao, DateTime dataCriacao, DateTime dataConclusao, Status status)
    {
        TarefaId = tarefaId;
        Titulo = titulo;
        Descricao = descricao;
        DataCriacao = dataCriacao;
        DataConclusao = dataConclusao;
        Status = status;
    }

    public TarefaDTO(Tarefa tarefa)
    {
        TarefaId = tarefa.TarefaId;
        Titulo = tarefa.Titulo;
        Descricao = tarefa.Descricao;
        DataCriacao = tarefa.DataCriacao;
        DataConclusao = tarefa.DataConclusao;
        Status = tarefa.Status;
    }
}