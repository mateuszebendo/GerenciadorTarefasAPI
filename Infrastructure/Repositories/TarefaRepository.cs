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
    
    public async Task<Tarefa> Post(Tarefa tarefa)
    {
        try
        {
            string sqlQuery = "INSERT INTO Tarefas(titulo, descricao, dataCriacao, dataConclusao, Status)" +
                              "VALUES (@titulo, @descricao, @dataCriacao, @dataConclusao, @Status) " +
                              "RETURNING tarefaid";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                tarefa.TarefaId =  await connection.ExecuteScalarAsync<int>(sqlQuery, tarefa);
                return tarefa;
            }
        }
        catch (PostgresException error)
        {
            throw new ApplicationException("Um erro aconteceu durante a query SQL: " + error.Message);
        }
    }
    
    public async Task<IEnumerable<Tarefa>> Get()
    {
        try
        {
            string sqlQuery = "SELECT * FROM Tarefas";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Tarefa>(sqlQuery);
            }
        }
        catch (PostgresException error)
        {
            throw new ApplicationException("Um erro aconteceu durante a query SQL: " + error.Message);
        }
    }

    public async Task<IEnumerable<Tarefa>> GetAlphabetically()
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
            throw new ApplicationException("Um erro aconteceu durante a query SQL: " + error.Message);
        }
    }

    public async Task<Tarefa> GetById(int id)
    {
        try
        {
            string sqlQuery = "SELECT * FROM Tarefas WHERE tarefaid = @Id";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Tarefa>(sqlQuery, new {Id = id});
            }
        }
        catch (PostgresException error)
        {
            throw new ApplicationException("Um erro aconteceu durante a query SQL: " + error.Message);
        }
    }

    public async Task<bool> Put(Tarefa tarefa, int id)
    {
        try
        {
            string sqlQuery = @"UPDATE tarefas
                                SET titulo = @titulo, 
                                    descricao = @descricao, 
                                    dataconclusao = @dataConclusao,
                                    status = @status
                                WHERE tarefaid = @Id";

            var parametros = new DynamicParameters();
            
            parametros.Add("titulo", tarefa.Titulo);
            parametros.Add("descricao", tarefa.Descricao);
            parametros.Add("dataConclusao", tarefa.DataConclusao);
            parametros.Add("status", tarefa.Status);
            parametros.Add("Id", id);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.ExecuteAsync(sqlQuery, parametros) > 0;
            }
        }
        catch (PostgresException error)
        {
            throw new ApplicationException("Um erro aconteceu durante a query SQL: " + error.Message);
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            string sqlQuery = "DELETE FROM tarefas where tarefaid = @Id";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.ExecuteAsync(sqlQuery, new { Id = id }) > 0;
            }
        }
        catch (PostgresException error)
        {
            throw new ApplicationException("Um erro aconteceu durante a query SQL: " + error.Message);
        }
    }
}