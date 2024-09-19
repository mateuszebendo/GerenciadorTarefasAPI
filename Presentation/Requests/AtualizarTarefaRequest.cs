namespace Presentation.Requests;

public class AtualizarTarefaRequest
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }

    public AtualizarTarefaRequest(string titulo, string descricao)
    {
        Titulo = titulo;
        Descricao = descricao;
    }
}