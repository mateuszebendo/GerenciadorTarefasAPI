using Domain.Entities;

namespace Domain.Contracts;

public interface IUsuarioRepository
{
    public Task<bool> RegistrarUsuario(Usuario usuario);
    public Task<IEnumerable<Usuario>> RecuperarUsuariosAtivos();
    public Task<IEnumerable<Usuario>> RecuperarUsuariosAlfabeticamente();
    public Task<Usuario> RecuperarUsuarioPorId(int id);
    // public Task<bool> Put(Usuario usuario, int id);
    public Task<bool> DesativarUsuario(int id);
}