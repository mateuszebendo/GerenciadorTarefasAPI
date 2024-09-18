using Domain.Enums;

namespace Domain.Entities;

public class Tarefa
{
    public int TarefaId { get; set; }
    public String Titulo { get; set; }
    public String Descricao { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataConclusao { get; set; }
    public Status Status { get; set; }
    
    public int UsuarioId { get; set; }
    
    public Tarefa() {}

    public Tarefa(int tarefaId, string titulo, string descricao, DateTime dataCriacao, DateTime dataConclusao, Status status, int usuarioId)
    {
        TarefaId = tarefaId;
        Titulo = titulo;
        Descricao = descricao;
        DataCriacao = dataCriacao;
        DataConclusao = dataConclusao;
        Status = status;
        UsuarioId = usuarioId;
    }

    public Tarefa(string titulo, string descricao, Status status, int usuarioId)
    {
        Titulo = titulo;
        Descricao = descricao;
        DataCriacao = DateTime.Now;  
        Status = status;
        UsuarioId = usuarioId;
    }

    public Tarefa(string titulo, DateTime dataConclusao, string descricao, int usuarioId)
    {
        Titulo = titulo;
        Descricao = descricao;
        DataConclusao = dataConclusao;
        UsuarioId = usuarioId;
    }
}