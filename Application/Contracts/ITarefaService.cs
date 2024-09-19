using Application.DTOs;

namespace Application.Contracts;

public interface ITarefaService
{
    Task<TarefaDTO> CriarTarefa(CriarTarefaDTO criarTarefaDto);
    Task<List<TarefaDTO>> RecuperarTarefas(string usuarioId);
    Task<TarefaDTO> RecuperarTarefaPorId(string id, string usuarioId);
    Task<TarefaDTO> AlterarTarefa(AtualizarTarefaDTO atualizarTarefaDto, string id);
    Task<string> ColocarTarefaEmAndamento(string id, string usuarioId);
    Task<string> ConcluirTarefa(string id, string usuarioId);
    Task<string> DeletarTarefa(string id, string usuarioId);
}