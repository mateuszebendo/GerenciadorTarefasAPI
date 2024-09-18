namespace Presentation.Requests;

public class AtualizarTarefaRequest
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime DataConclusao { get; set; }
    
    public int UsuarioId { get; set; }

    public AtualizarTarefaRequest(string titulo, string descricao, DateTime dataConclusao, int usuarioId)
    {
        Titulo = titulo;
        Descricao = descricao;
        DataConclusao = dataConclusao;
        UsuarioId = usuarioId;
    }
}