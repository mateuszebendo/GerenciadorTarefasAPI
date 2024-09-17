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
    private readonly IMapper _mapper;

    public TarefaService(ITarefaRepository tarefaRepository, IMapper mapper)
    {
        _tarefaRepository = tarefaRepository;
        _mapper = mapper;
    }
    
    public async Task<TarefaDTO> Post(CriarTarefaDTO criarTarefaDto)
    {
        try
        {
            Tarefa tarefa = new Tarefa(criarTarefaDto.Titulo, criarTarefaDto.Descricao, criarTarefaDto.Status);

            tarefa = await _tarefaRepository.Post(tarefa);

            return _mapper.Map<TarefaDTO>(tarefa);
        }
        catch (ArgumentException error)
        {
            throw new ApplicationException("Ocorreu um erro com os dados fornecidos." + error.Message);
        }
    }

    public async Task<List<TarefaDTO>> Get()
    {
        try
        {
            IEnumerable<Tarefa> tarefas = await _tarefaRepository.Get();

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

    public async Task<TarefaDTO> GetById(int id)
    {
        try
        {
            Tarefa tarefa = await _tarefaRepository.GetById(id);

            if (tarefa.Titulo.LongCount() == 0)
            {
                throw new Exception("Não foi possível resgatar a tarefa");
            }

            return _mapper.Map<TarefaDTO>(tarefa);
        } catch (ArgumentException error)
        {
            throw new ApplicationException("Ocorreu ao recuperar a tarefa" + error.Message);
        }
    }

    public async Task<TarefaDTO> Put(AtualizarTarefaDTO atualizarTarefaDto, int id)
    {
        try
        {
            Tarefa tarefa = new Tarefa(atualizarTarefaDto.Titulo,
                atualizarTarefaDto.DataConclusao, atualizarTarefaDto.Descricao);

            if (await _tarefaRepository.Put(tarefa, id))
            {
                tarefa = await _tarefaRepository.GetById(id);
                return _mapper.Map<TarefaDTO>(tarefa);
            }

            throw new Exception("Ocorreu um erro ao tentar atualizar a Tarefa");
        } catch (ArgumentException error)
        {
            throw new ApplicationException("Ocorreu ao atualizar a tarefa" + error.Message);
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            return await _tarefaRepository.Delete(id);
        } catch (ArgumentException error)
        {
            throw new ApplicationException("Ocorreu ao atualizar a tarefa" + error.Message);
        }
    }
}