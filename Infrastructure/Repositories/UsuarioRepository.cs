using Dapper;
using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Config;
using Npgsql;

namespace Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly string _connectionString;

    public UsuarioRepository()
    {
        _connectionString = DataBaseConfig.ConnectionString;
    }
    
    public async Task<bool> RegistrarUsuario(Usuario usuario)
    {
        try
        {
            string sqlQuery = "INSERT INTO Usuarios(Nome, Status, Email, Senha)" +
                              "VALUES (@nome, @status, @email, @senha)";
            
            var parametros = new DynamicParameters();
            
            parametros.Add("nome", usuario.Nome);
            parametros.Add("status", usuario.Status.ToString());
            parametros.Add("email", usuario.Email);
            parametros.Add("senha", usuario.Senha);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.ExecuteAsync(sqlQuery, parametros) > 0;
            }
        }
        catch (PostgresException error)
        {
            throw new NpgsqlException("Ocorreu um erro na requisição: " + error.MessageText);
        }
    }
        
    public async Task<IEnumerable<Usuario>> RecuperarUsuariosAtivos()
    {
        try
        {
            string sqlQuery = "SELECT * FROM Usuarios WHERE Status = 'Ativo'";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Usuario>(sqlQuery);
            }
        }
        catch (PostgresException error)
        {
            throw new NpgsqlException("Ocorreu um erro na requisição: " + error.MessageText);
        }
    }

    public async Task<IEnumerable<Usuario>> RecuperarUsuariosAlfabeticamente()
    {
        try
        {
            string sqlQuery = "SELECT * FROM Usuarios ORDER BY Nome ASC";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Usuario>(sqlQuery);
            }
        }
        catch (PostgresException error)
        {
            throw new NpgsqlException("Ocorreu um erro na requisição: " + error.MessageText);
        }
    }

    public async Task<Usuario> RecuperarUsuarioPorId(int id)
    {
        try
        {
            string sqlQuery = "SELECT * FROM Usuarios WHERE usuarioId = @Id";
            
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Usuario>(sqlQuery, new {Id = id});
            }
        }
        catch (PostgresException error)
        {
            throw new NpgsqlException("Ocorreu um erro na requisição: " + error.MessageText);
        }
    }

    // public async Task<bool> Put(Usuario usuario, int id)
    // {
    //     try
    //     {
    //         string sqlQuery = @"UPDATE Usuarios
    //                             SET Nome = @nome
    //                                 Status = @status
    //                                 Email = @email 
    //                                 Senha = @senha
    //                             WHERE UsuarioId = @Id";
    //
    //         var parametros = new DynamicParameters();
    //         
    //         parametros.Add("nome", usuario.Nome);
    //         parametros.Add("status", usuario.Status.ToString());
    //         parametros.Add("email", usuario.Email);
    //         parametros.Add("senha", usuario.Senha);
    //         parametros.Add("Id", id);
    //
    //         using (var connection = new NpgsqlConnection(_connectionString))
    //         {
    //             return await connection.ExecuteAsync(sqlQuery, parametros) > 0;
    //         }
    //     }
    //     catch (PostgresException error)
    //     {
    //         throw new NpgsqlException("Ocorreu um erro na requisição: " + error.MessageText);
    //     }
    // }

    public async Task<bool> DesativarUsuario(int id)
    {
        try
        {
            string sqlQuery = "DELETE FROM Usuarios where UsuarioId = @Id";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.ExecuteAsync(sqlQuery, new { Id = id }) > 0;
            }
        }
        catch (PostgresException error)
        {
            throw new NpgsqlException("Um erro aconteceu durante a query SQL: " + error.Message);
        }
    }
}