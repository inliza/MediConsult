using System.Data.SqlClient;

namespace MediConsult.Domain.Primitives;

public interface IBaseRepository<T>
{
    Task<List<T>?> GetAll(string tableName);
    Task<T?> Get(string tableName, int id);

    Task<T?> GetByProperty(string tableName, string propertyName, object propertyValue);
    Task<T?> Insert(string query, T entity);
    Task<T?> Delete(string query, int id);
    Task DeleteLogic(string tableName, int id);
    Task Insert(string query, List<SqlParameter> parameters);
    Task Update(string query, List<SqlParameter> parameters);
}
