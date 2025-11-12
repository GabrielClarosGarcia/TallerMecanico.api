using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TallerMecanico.Core.Entities;
using TallerMecanico.Core.Interfaces;

namespace TallerMecanico.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IDbConnection _dbConnection;

        public ClientRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM clients WHERE IdClient = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Client>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            var sql = "SELECT * FROM clients";
            return await _dbConnection.QueryAsync<Client>(sql);
        }

        public async Task AddAsync(Client entity)
        {
            var sql = "INSERT INTO clients (Name) VALUES (@Name)";
            await _dbConnection.ExecuteAsync(sql, entity);
        }

        public void Update(Client entity)
        {
            var sql = "UPDATE clients SET Name = @Name WHERE IdClient = @IdClient";
            _dbConnection.Execute(sql, entity);
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM clients WHERE IdClient = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<Client> GetByEmailAsync(string email)
        {
            var sql = "SELECT * FROM clients WHERE Name = @Email LIMIT 1";
            return await _dbConnection.QueryFirstOrDefaultAsync<Client>(sql, new { Email = email });
        }

        public async Task<IEnumerable<Client>> GetByNameAsync(string name)
        {
            var sql = "SELECT * FROM clients WHERE Name LIKE @Name";
            return await _dbConnection.QueryAsync<Client>(sql, new { Name = "%" + name + "%" });
        }
    }
}
