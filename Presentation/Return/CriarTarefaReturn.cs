namespace Presentation.Return;

public class CriarTarefaReturn
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string DataCriacao { get; set; }
    public string Status { get; set; }

    public CriarTarefaReturn(int id, string titulo, string descricao, string dataCriacao, string status)
    {
        Id = id;
        Titulo = titulo;
        Descricao = descricao;
        DataCriacao = dataCriacao;
        Status = status;
    }
}