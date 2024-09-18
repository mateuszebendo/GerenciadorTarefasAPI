using Domain.Entities;

namespace Domain.Contracts;

public interface IUsuarioRepository
{
    public Task<IEnumerable<Usuario>> Get();
    public Task<IEnumerable<Usuario>> GetAlphabetically();
    public Task<Usuario> GetById(int id);
    public Task<Usuario> Post(Usuario tarefa);
    public Task<bool> Put(Usuario tarefa, int id);
    public Task<bool> Delete(int id);
}