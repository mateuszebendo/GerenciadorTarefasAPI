using Application.Contracts;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public async Task<IActionResult> CriarTarefa([FromBody] CriarTarefaRequest criarTarefaRequest)
    {
        var usuarioId = User.FindFirst("UsuarioId")?.Value;
        CriarTarefaDTO criarTarefaDto = new CriarTarefaDTO(criarTarefaRequest.Titulo, criarTarefaRequest.Descricao, usuarioId);

        TarefaDTO tarefaDto = await _tarefaService.CriarTarefa(criarTarefaDto);

        return Ok(new CriarTarefaReturn(tarefaDto.TarefaId, tarefaDto.Titulo, tarefaDto.Descricao,tarefaDto.DataCriacao.Date.ToString(), tarefaDto.Status.ToString()));
    }

    [HttpGet("recuperar-tarefas")]
    [Authorize]
    public async Task<IActionResult> RecuperarTarefas()
    {
        var usuarioId = User.FindFirst("UsuarioId")?.Value;
        List<TarefaReturn> listaReturn = new List<TarefaReturn>();
        
        foreach (var tarefaDto in await _tarefaService.RecuperarTarefas(usuarioId))
        {
            listaReturn.Add(new TarefaReturn(tarefaDto));
        }

        return Ok(listaReturn);
    }
    
    [HttpGet("recuperar-tarefa/{tarefaId}")]
    [Authorize]
    public async Task<IActionResult> RecuperarTarefaPorId(int tarefaId)
    {
        var usuarioId = User.FindFirst("UsuarioId")?.Value;
        var tarefaReturn = new TarefaReturn(await _tarefaService.RecuperarTarefaPorId(tarefaId.ToString(), usuarioId));

        return Ok(tarefaReturn);
    }

    [HttpPut("alterar-tarefa/{tarefaId}")]
    [Authorize]
    public async Task<IActionResult> AlterarTarefa([FromBody] AtualizarTarefaRequest atualizarTarefaRequest, int tarefaId)
    {
        var usuarioId = User.FindFirst("UsuarioId")?.Value;
        AtualizarTarefaDTO atualizarTarefaDto = new AtualizarTarefaDTO(atualizarTarefaRequest.Titulo,
            atualizarTarefaRequest.Descricao, usuarioId);
        
        return Ok(new TarefaReturn(await _tarefaService.AlterarTarefa(atualizarTarefaDto, tarefaId.ToString())));
    }

    [HttpPut("tarefa-em-andamento/{tarefaId}")]
    [Authorize]
    public async Task<IActionResult> ColocarTarefaEmAndamento(int tarefaId)
    {
        try
        {
            var usuarioId = User.FindFirst("UsuarioId")?.Value;
            string tituloTarefa = await _tarefaService.ColocarTarefaEmAndamento(tarefaId.ToString(), usuarioId);
            return Ok($"Tarefa colocada '{tituloTarefa}' em andamento.");
        }
        catch (Exception error)
        {
            return BadRequest("Erro na requisição: " + error);
        }
        
    }

    [HttpPut("concluir-tarefa/{tarefaId}")]
    [Authorize]
    public async Task<IActionResult> ConcluirTarefa(int tarefaId)
    {
        try
        {
            var usuarioId = User.FindFirst("UsuarioId")?.Value;
            string tituloTarefa = await _tarefaService.ConcluirTarefa(tarefaId.ToString(), usuarioId);
            return Ok($"Tarefa '{tituloTarefa}' concluída com sucesso");
        }
        catch (Exception error)
        {
            return BadRequest("Error ao concluir a tarefa: " + error);
        }
    }

    [HttpDelete("deletar-tarefa/{tarefaId}")]
    [Authorize]
    public async Task<IActionResult> Delete(int tarefaId)
    {
        try
        {
            var usuarioId = User.FindFirst("UsuarioId")?.Value;
            string tituloTarefa = await _tarefaService.DeletarTarefa(tarefaId.ToString(), usuarioId);
            return Ok($"Tarefa '{tituloTarefa}' deletada com sucesso!");
        }
        catch (Exception error)
        {
            return NotFound("Não foi possível deletar a tarefa, error: " + error);
        }
    }
}