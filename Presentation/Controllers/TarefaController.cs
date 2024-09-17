using Application.Contracts;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests;
using Presentation.Return;

namespace Presentation.Controllers;

[ApiController]
[Route("tarefa")]
public class TarefaController : ControllerBase
{
    private readonly ITarefaService _tarefaService;

    public TarefaController(ITarefaService tarefaService)
    {
        _tarefaService = tarefaService;
    }

    [HttpPost("criar-tarefa")]
    public async Task<IActionResult> Post([FromBody] CriarTarefaRequest criarTarefaRequest)
    {
        CriarTarefaDTO criarTarefaDto = new CriarTarefaDTO(criarTarefaRequest.Titulo, criarTarefaRequest.Descricao);

        TarefaDTO tarefaDto = await _tarefaService.Post(criarTarefaDto);

        return Ok(new CriarTarefaReturn(tarefaDto.TarefaId, tarefaDto.Titulo, tarefaDto.Descricao,tarefaDto.DataCriacao.Date.ToString(), tarefaDto.Status.ToString()));
    }

    [HttpGet("recuperar-tarefas")]
    public async Task<IActionResult> Get()
    {
        List<TarefaReturn> listaReturn = new List<TarefaReturn>();
        
        foreach (var tarefaDto in await _tarefaService.Get())
        {
            listaReturn.Add(new TarefaReturn(tarefaDto));
        }

        return Ok(listaReturn);
    }
    
    [HttpGet("recuperar-tarefa/{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var tarefaReturn = new TarefaReturn(await _tarefaService.GetById(id));

        return Ok(tarefaReturn);
    }

    [HttpPut("alterar-tarefa/{id}")]
    public async Task<IActionResult> Put([FromBody] AtualizarTarefaRequest atualizarTarefaRequest, [FromRoute] int id)
    {
        AtualizarTarefaDTO atualizarTarefaDto = new AtualizarTarefaDTO(atualizarTarefaRequest.Titulo,
            atualizarTarefaRequest.Descricao, atualizarTarefaRequest.DataConclusao);
        
        return Ok(new TarefaReturn(await _tarefaService.Put(atualizarTarefaDto, id)));
    }

    [HttpDelete("deletar-tarefa/{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (await _tarefaService.Delete(id))
        {
            return Ok("Tarefa deltada com sucesso");
        }

        return NotFound("Não foi possível deletar a tarefa");
    }
}