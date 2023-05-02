using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Product.Data.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;
        public SqlDataAccess(IConfiguration config)
        {
            _config = config;

        }
        public async Task<IEnumerable<T>> GetData<T, P>(string spName, P parameters,
            string connectionId="conn")// fetch stored procedure either collection of data or single row T= type of return data p=type of parameter 
        {
            using IDbConnection connection = new SqlConnection
                (_config.GetConnectionString(connectionId));
            return await connection.QueryAsync<T>(spName, parameters, commandType:
                CommandType.StoredProcedure);
        }

        public async Task SaveData<T>(string spName, T parameters,string connectionId="conn")//INSERTION OR UPDATION WILL PERFORM HERE
        {
            using IDbConnection connection = new SqlConnection
              (_config.GetConnectionString(connectionId));
            await connection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);

        }


    }
}
