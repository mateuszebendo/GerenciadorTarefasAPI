using Domain.Entities;

namespace Domain.Contracts;

public interface ITarefaRepository
{
    Task<Tarefa> CriarTarefa(Tarefa tarefa);
    Task<IEnumerable<Tarefa>> RecuperarTarefas(int usuarioId);
    Task<IEnumerable<Tarefa>> RecuperarTarefasAlfabeticamente();
    Task<Tarefa> RecuperarTarefaPorId(int id, int usuarioId);
    Task<Tarefa> AlterarTarefa(Tarefa tarefa, int tarefaId);
    Task<string> ColocarTarefaEmAndamento(int tarefaId, int usuarioId);
    Task<string> ConcluirTarefa(int tarefaId, int usuarioId);
    Task<string> DeletarTarefa(int tarefaId, int usuarioId);
}