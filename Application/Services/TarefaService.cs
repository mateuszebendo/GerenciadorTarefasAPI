using Application.Contracts;
using Application.DTOs;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

public class TarefaService : ITarefaService
{
    private readonly ITarefaRepository _tarefaRepository;
    private readonly ITarefaDomainService _tarefaDomainService;
    private readonly IMapper _mapper;

    public TarefaService(ITarefaRepository tarefaRepository, ITarefaDomainService tarefaDomainService, IMapper mapper)
    {
        _tarefaRepository = tarefaRepository;
        _mapper = mapper;
        _tarefaDomainService = tarefaDomainService;
    }
    
    public async Task<TarefaDTO> CriarTarefa(CriarTarefaDTO criarTarefaDto)
    {
        try
        {
            List<string> listaTarefa = new List<string>();
            foreach (var antigaTarefa in await _tarefaRepository.RecuperarTarefasAlfabeticamente())
            {
                listaTarefa.Add(antigaTarefa.Titulo);
            }
            
            _tarefaDomainService.VerificaDuplicidadeTitulo(listaTarefa, criarTarefaDto.Titulo);
            
            Tarefa tarefa = new Tarefa(criarTarefaDto.Titulo, criarTarefaDto.Descricao, criarTarefaDto.Status, int.Parse(criarTarefaDto.UsuarioId));

            tarefa = await _tarefaRepository.CriarTarefa(tarefa);

            return _mapper.Map<TarefaDTO>(tarefa);
        }
        catch (ArgumentException error)
        {
            throw new ApplicationException("Ocorreu um erro com os dados fornecidos." + error.Message);
        }
    }

    public async Task<List<TarefaDTO>> RecuperarTarefas(string usuarioId)
    {
        try
        {
            IEnumerable<Tarefa> tarefas = await _tarefaRepository.RecuperarTarefas(int.Parse(usuarioId));

            if (tarefas.LongCount() == 0)
            {
                throw new Exception("Nenhuma tarefa encontrada");
            }

            List<TarefaDTO> tarefasDTO = new List<TarefaDTO>();
            foreach (var tarefa in tarefas)
            {
                tarefasDTO.Add(new TarefaDTO(tarefa));
            }

            return tarefasDTO;
        } catch (ArgumentException error)
        {
            throw new ApplicationException("Ocorreu ao recuperar a lista de Tarefas" + error.Message);
        }
    }

    public async Task<TarefaDTO> RecuperarTarefaPorId(string tarefaId, string usuarioId)
    {
        try
        {
            Tarefa tarefa = await _tarefaRepository.RecuperarTarefaPorId(int.Parse(tarefaId), int.Parse(usuarioId));

            if (tarefa.Titulo.LongCount() == 0)
            {
                throw new Exception("Não foi encontrada nenhuma recuperar a tarefa com esse Id");
            }

            return _mapper.Map<TarefaDTO>(tarefa);
        } catch (ArgumentException error)
        {
            throw new ApplicationException("Ocorreu ao recuperar a tarefa" + error.Message);
        }
    }

    public async Task<TarefaDTO> AlterarTarefa(AtualizarTarefaDTO atualizarTarefaDto, string tarefaId)
    {
        try
        {
            Tarefa tarefa = new Tarefa(atualizarTarefaDto.Titulo, atualizarTarefaDto.Descricao, int.Parse(atualizarTarefaDto.UsuarioId));

            tarefa = await _tarefaRepository.AlterarTarefa(tarefa, int.Parse(tarefaId));
            return _mapper.Map<TarefaDTO>(tarefa);
        } catch (ArgumentException error)
        {
            throw new ApplicationException("Ocorreu ao atualizar a tarefa" + error.Message);
        }
    }

    public async Task<string> ColocarTarefaEmAndamento(string tarefaId, string usuarioId)
    {
        try
        {
            return await _tarefaRepository.ColocarTarefaEmAndamento(int.Parse(tarefaId), int.Parse(usuarioId));
        }
        catch (ArgumentException error)
        {
            throw new ApplicationException("Ocorreu ao atualizar a tarefa para pendente: " + error.Message);
        }
    }
    
    public async Task<string> ConcluirTarefa(string tarefaId, string usuarioId)
    {
        try
        {
            return await _tarefaRepository.ConcluirTarefa(int.Parse(tarefaId), int.Parse(usuarioId));
        }
        catch (ArgumentException error)
        {
            throw new ApplicationException("Ocorreu ao atualizar a tarefa para pendente: " + error.Message);
        }
    }

    public async Task<string> DeletarTarefa(string tarefaId, string usuarioId)
    {
        try
        {
            return await _tarefaRepository.DeletarTarefa(int.Parse(tarefaId), int.Parse(usuarioId));
        } catch (ArgumentException error)
        {
            throw new ApplicationException("Ocorreu ao atualizar a tarefa" + error.Message);
        }
    }
}