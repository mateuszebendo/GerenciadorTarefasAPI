using Application.DTOs;

namespace Application.Contracts;

public interface IUsuarioService
{
    Task<bool> RegistrarUsuario(CriarUsuarioDTO criarUsuarioDto);
    Task<bool> LogarUsuario(LoginUsuarioDTO loginUsuarioDto);
    Task<UsuarioDTO> RecuperarInformacoesUsuario(int id);
    // Task<bool> Put(UsuarioDTO usuarioDto, int id);
    Task<bool> DesativarUsuario(int id);
}