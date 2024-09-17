using Application.DTOs;

namespace Application.Contracts;

public interface ITarefaService
{
    public Task<TarefaDTO> Post(CriarTarefaDTO criarTarefaDto);
    public Task<List<TarefaDTO>> Get();
    public Task<TarefaDTO> GetById(int id);
    public Task<TarefaDTO> Put(AtualizarTarefaDTO atualizarTarefaDto, int id);
    public Task<bool> Delete(int id);
}