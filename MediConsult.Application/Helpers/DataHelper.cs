using MediConsult.Application.Interfaces;
using System.Data.SqlClient;
using System.Reflection;


namespace MediConsult.Application.Helpers
{
    public class DataHelper : IDataHelper
    {
        public List<SqlParameter> GetSqlParameters<T>(T entity)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var sqlParameters = new List<SqlParameter>();

            foreach (var property in properties)
            {
                var value = property.GetValue(entity);
                var parameter = new SqlParameter("@" + property.Name, value ?? DBNull.Value);
                sqlParameters.Add(parameter);
            }

            return sqlParameters;
        }
    }
}
