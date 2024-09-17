using Domain.Entities;

namespace Domain.Contracts;

public interface ITarefaRepository
{
    public Task<IEnumerable<Tarefa>> Get();
    public Task<IEnumerable<Tarefa>> GetAlphabetically();
    public Task<Tarefa> GetById(int id);
    public Task<Tarefa> Post(Tarefa tarefa);
    public Task<bool> Put(Tarefa tarefa, int id);
    public Task<bool> Delete(int id);
}