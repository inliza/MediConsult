using Dapper;
using MediConsult.Domain.Primitives;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Xml.Linq;

namespace MediConsult.Infrastructure.Abstractions;

public class BaseRepository<T> : IBaseRepository<T>
{
    private readonly IAppSettings _appSettings;
    public BaseRepository(
        IAppSettings appSettings)
    {
        _appSettings = appSettings;
    }
    public async Task<T?> Delete(string query, int id)
    {
        using var connection = new SqlConnection(_appSettings.ConnectionString);
        var result = await connection.ExecuteScalarAsync<T>(query, new { id });
        if (result != null)
        {
            return result;
        }
        return default(T);
    }

    public async Task DeleteLogic(string tableName, int id)
    {
        string sql = $"UPDATE {tableName} SET IsDeleted = 1 WHERE Id = @Id";
        using var connection = new SqlConnection(_appSettings.ConnectionString);
        int rowsAffected = await connection.ExecuteAsync(sql, new { id });
        if (rowsAffected > 0)
        {
            Console.WriteLine($"Logically deleted record successfully");
        }

    }

    public async Task<T?> Get(string tableName, int id)
    {
        using var connection = new SqlConnection(_appSettings.ConnectionString);
        string query = $"SELECT * FROM {tableName} WHERE ID = @id AND IsDeleted = 0";
        var result = await connection.QueryFirstOrDefaultAsync<T>(query, new { id });
        if (result != null)
        {
            return result;
        }
        return default(T);
    }

    public async Task<T?> GetByProperty(string tableName, string propertyName, object propertyValue)
    {
        using var connection = new SqlConnection(_appSettings.ConnectionString);
        var query = $"SELECT * FROM {tableName} WHERE {propertyName} = @propertyValue";
        var result = await connection.QueryFirstOrDefaultAsync<T>(query, new { propertyValue });
        if (result != null)
        {
            return result;
        }
        return default(T);
    }

    public async Task<List<T>?> GetAll(string tableName)
    {
        using var connection = new SqlConnection(_appSettings.ConnectionString);
        string query = $"SELECT * FROM {tableName} WHERE IsDeleted = 0";
        var result = await connection.QueryAsync<T>(query);
        if (result != null && result.Any())
        {
            return result.ToList();
        }
        return default(List<T>);
    }

    public async Task<T?> Insert(string query, T entity)
    {
        using var connection = new SqlConnection(_appSettings.ConnectionString);
        var result = await connection.ExecuteScalarAsync<T>(query, new { entity });
        if (result != null)
        {
            return result;
        }
        return default(T);
    }

    public async Task Insert(string query, List<SqlParameter> parameters)
    {
        using var connection = new SqlConnection(_appSettings.ConnectionString);
        using var cmd = new SqlCommand(query, connection);
        cmd.CommandType = CommandType.StoredProcedure;
        foreach (SqlParameter parameter in parameters)
        {
            cmd.Parameters.Add(parameter);
        }
        connection.Open();
        int res = await cmd.ExecuteNonQueryAsync();

        if (res > 0)
        {
            Console.WriteLine($"Procedure to insert entity execute succesfully {query}");
        }
    }
    public async Task Update(string query, List<SqlParameter> parameters)
    {
        using var connection = new SqlConnection(_appSettings.ConnectionString);
        using var cmd = new SqlCommand(query, connection);
        cmd.CommandType = CommandType.StoredProcedure;
        foreach (SqlParameter parameter in parameters)
        {
            cmd.Parameters.Add(parameter);
        }
        connection.Open();
        int res = await cmd.ExecuteNonQueryAsync();

        if (res > 0)
        {
            Console.WriteLine($"Procedure to update entity execute succesfully {query}");
        }
    }

}
