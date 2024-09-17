using Application.DTOs;

namespace Presentation.Return;

public class TarefaReturn
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string DataCriacao { get; set; }
    public string DataConclusao { get; set; }
    public string Status { get; set; }

    public TarefaReturn(TarefaDTO tarefaDto)
    {
        Id = tarefaDto.TarefaId;
        Titulo = tarefaDto.Titulo;
        Descricao = tarefaDto.Descricao;
        DataCriacao = tarefaDto.DataCriacao.Date.ToString();
        DataConclusao = tarefaDto.DataConclusao.Date.ToString();
        Status = tarefaDto.Status.ToString();
    }
} 