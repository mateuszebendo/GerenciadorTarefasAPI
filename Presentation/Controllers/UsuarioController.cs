using Application.Contracts;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests;

namespace Presentation.Controllers;

[ApiController]
[Route("usuario")]
public class UsuarioController : ControllerBase
{
    // private readonly IUsuarioService _usuarioService;
    //
    // public UsuarioController(IUsuarioService usuarioService)
    // {
    //     _usuarioService = usuarioService;
    // }
    //
    // [HttpPost("criar-usuario")]
    // public async Task<IActionResult> Post([FromBody] CriarUsuarioRequest criarUsuarioRequest)
    // {
    //     var criarUsuarioDTO = new CriarUsuarioDTO(criarUsuarioRequest.Nome, criarUsuarioRequest.Email, criarUsuarioRequest.Senha);
    //     return Ok(await _usuarioService.Post(criarUsuarioDTO));
    // }
    //
    // [HttpGet("recuperar-usuarios")]
    // public async Task<IActionResult> Get()
    // {
    //     return Ok(await _usuarioService.Get());
    // }
    //
    // [HttpGet("recupera-usuario/{id}")]
    // public async Task<IActionResult> GetById([FromRoute] int id)
    // {
    //     return Ok(await _usuarioService.GetById(id));
    // }

    // [HttpPut("atualizar-usuario/{id}")]
    // public async Task<IActionResult> Put([FromRoute] int id, [FromBody] AtualizarUsuarioRequest atualizarUsuarioRequest)
    // {
    //     
    // }
    //
    // [HttpDelete("deletar-usuario/{id}")]
    // public async Task<IActionResult> Delete([FromRoute] int id)
    // {
    //     if (await _usuarioService.Delete(id))
    //     {
    //         return Ok("Usuário deletado com sucesso");
    //     }
    //     return NotFound("Não foi possível achar o usuário");
    // }
}