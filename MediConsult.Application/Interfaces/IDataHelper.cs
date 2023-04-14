using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediConsult.Application.Interfaces
{
    public interface IDataHelper
    {
        List<SqlParameter> GetSqlParameters<T>(T entity);
    }
}
