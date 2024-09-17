namespace Presentation.Requests;

public class AtualizarTarefaRequest
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime DataConclusao { get; set; }

    public AtualizarTarefaRequest(string titulo, string descricao, DateTime dataConclusao)
    {
        Titulo = titulo;
        Descricao = descricao;
        DataConclusao = dataConclusao;
    }
}