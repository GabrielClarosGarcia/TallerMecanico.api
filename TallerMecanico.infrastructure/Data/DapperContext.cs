using System.Data;
using Dapper;
using TallerMecanico.Core.Interfaces;

namespace TallerMecanico.Infrastructure.Data
{
    public class DapperContext : IDapperContext
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DapperContext(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
        {
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryAsync<T>(sql, parameters);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? parameters = null)
        {
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
        }
    }
}
