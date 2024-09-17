using Application.DTOs;

namespace Application.Contracts;

public interface ITarefaService
{
    Task<TarefaDTO> Post(CriarTarefaDTO criarTarefaDto);
    Task<List<TarefaDTO>> Get();
    Task<TarefaDTO> GetById(int id);
    Task<TarefaDTO> Put(AtualizarTarefaDTO atualizarTarefaDto, int id);
    Task<bool> Delete(int id);
}