using Application.DTOs;

namespace Application.Contracts;

public interface IUsuarioService
{
    Task<UsuarioDTO> Post(CriarUsuarioDTO criarUsuarioDto);
    Task<List<UsuarioDTO>> Get();
    Task<UsuarioDTO> GetById(int id);
    Task<bool> Put(UsuarioDTO usuarioDto, int id);
    Task<bool> Delete(int id);
}