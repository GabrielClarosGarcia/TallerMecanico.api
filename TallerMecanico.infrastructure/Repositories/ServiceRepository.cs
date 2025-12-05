using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TallerMecanico.Core.Entities;
using TallerMecanico.Core.Interfaces;

namespace TallerMecanico.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly IDbConnection _dbConnection;

        public ServiceRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            var sql = "SELECT * FROM services";
            return await _dbConnection.QueryAsync<Service>(sql);
        }

        public async Task<IEnumerable<Service>> GetServicesByVehicleAsync(int vehicleId)
        {
            var sql = "SELECT * FROM services WHERE IdVehicle = @VehicleId";
            return await _dbConnection.QueryAsync<Service>(sql, new { VehicleId = vehicleId });
        }

        public async Task<Service> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM services WHERE IdService = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Service>(sql, new { Id = id });
        }

        public async Task AddAsync(Service entity)
        {
            var sql = "INSERT INTO services (IdVehicle, Description, DateService) VALUES (@IdVehicle, @Description, @DateService)";
            await _dbConnection.ExecuteAsync(sql, entity);
        }

        public void Update(Service entity)
        {
            var sql = "UPDATE services SET Description = @Description, DateService = @DateService WHERE IdService = @IdService";
            _dbConnection.Execute(sql, entity);
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM services WHERE IdService = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
