using Dapper;
using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Config;
using Npgsql;

namespace Infrastructure.Repositories;

public class TarefaRepository : ITarefaRepository
{
    private readonly string _connectionString;

    public TarefaRepository()
    {
        _connectionString = DataBaseConfig.ConnectionString;
    }
    
    public async Task<Tarefa> CriarTarefa(Tarefa tarefa)
    {
        try
        {
            string sqlQuery = @"INSERT INTO Tarefas (titulo, descricao, dataCriacao, dataConclusao, Status, UsuarioId)
                    VALUES (@Titulo, @Descricao, @DataCriacao, @DataConclusao, @Status, @UsuarioId)
                    RETURNING tarefaid";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                int tarefaId = await connection.ExecuteScalarAsync<int>(sqlQuery, tarefa);
                tarefa.TarefaId = tarefaId;
                return tarefa;
            }
        }
        catch (PostgresException error)
        {
            throw new NpgsqlException("Um erro aconteceu durante a query SQL: " + error.Message);
        }
    }
    
    public async Task<IEnumerable<Tarefa>> RecuperarTarefas(int usuarioId)
    {
        try
        {
            string sqlQuery = "SELECT * FROM Tarefas WHERE UsuarioId = @Id";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Tarefa>(sqlQuery, new {Id = usuarioId});
            }
        }
        catch (PostgresException error)
        {
            throw new NpgsqlException("Um erro aconteceu durante a query SQL: " + error.Message);
        }
    }

    public async Task<IEnumerable<Tarefa>> RecuperarTarefasAlfabeticamente()
    {
        try
        {
            string sqlQuery = "SELECT * FROM Tarefas ORDER BY Titulo ASC";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Tarefa>(sqlQuery);
            }
        }
        catch (PostgresException error)
        {
            throw new NpgsqlException("Um erro aconteceu durante a query SQL: " + error.Message);
        }
    }

    public async Task<Tarefa> RecuperarTarefaPorId(int tarefaId, int usuarioId)
    {
        try
        {
            string sqlQuery = "SELECT * FROM Tarefas WHERE TarefaId = @TarefaId AND UsuarioId = @UsuarioId";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Tarefa>(sqlQuery, new { TarefaId = tarefaId, UsuarioId = usuarioId });
            }

        }
        catch (PostgresException error)
        {
            throw new NpgsqlException("Um erro aconteceu durante a query SQL: " + error.Message);
        }
    }

    public async Task<Tarefa> AlterarTarefa(Tarefa tarefa, int tarefaId)
    {
        try
        {
            string sqlQuery = @"UPDATE tarefas
                                SET titulo = @Titulo, 
                                    descricao = @Descricao
                                WHERE tarefaid = @TarefaId AND usuarioid = @UsuarioId
                                RETURNING *";

            var parametros = new DynamicParameters();
            
            parametros.Add("Titulo", tarefa.Titulo);
            parametros.Add("Descricao", tarefa.Descricao);
            parametros.Add("TarefaId", tarefaId);
            parametros.Add("TarefaId", tarefa.UsuarioId);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Tarefa>(sqlQuery, parametros);
            }
        }
        catch (PostgresException error)
        {
            throw new NpgsqlException("Um erro aconteceu durante a query SQL: " + error.Message);
        }
    }

    public async Task<string> ColocarTarefaEmAndamento(int tarefaId, int usuarioId)
    {
        try
        {
            string sqlQuery = @"UPDATE Tarefas
                                SET status = 'EmAndamento' 
                                WHERE tarefaid = @TarefaId AND usuarioid = @UsuarioId
                                RETURNING titulo";
            
            var parametros = new DynamicParameters();
            parametros.Add("TarefaId", tarefaId);
            parametros.Add("UsuarioId", usuarioId);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<string>(sqlQuery, parametros);
            }
        }
        catch (PostgresException error)
        {
            throw new NpgsqlException("Um erro aconteceu durante a query SQL - " + error.Message);
        }
    }

    public async Task<string> ConcluirTarefa(int tarefaId, int usuarioId)
    {
        try
        {
            string sqlQuery = @"UPDATE Tarefas
                                SET status = 'Concluida' 
                                WHERE tarefaid = @TarefaId AND usuarioid = @UsuarioId
                                RETURNING titulo";

            var parametros = new DynamicParameters();
            
            parametros.Add("TarefaId", tarefaId);
            parametros.Add("UsuarioId", usuarioId);
            
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<string>(sqlQuery, parametros);
            }
        }
        catch (PostgresException error)
        {
            throw new NpgsqlException("Um erro aconteceu durante a query SQL - " + error.Message);
        }
    }

    public async Task<string> DeletarTarefa(int tarefaId, int usuarioId)
    {
        try
        {
            string sqlQuery = "DELETE FROM Tarefas " +
                              "WHERE TarefaId = @TarefaId AND UsuarioId = @UsuarioId " +
                              "RETURNING Titulo";

            var parametros = new DynamicParameters();
            
            parametros.Add("TarefaId", tarefaId);
            parametros.Add("UsuarioId", usuarioId);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<string>(sqlQuery, parametros);
            }
        }
        catch (PostgresException error)
        {
            throw new NpgsqlException("Um erro aconteceu durante a query SQL: " + error.Message);
        }
    }
}