using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TallerMecanico.Core.Entities;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Core.QueryFilters;

namespace TallerMecanico.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IDbConnection _dbConnection;

        public VehicleRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // Implementación de GetAllAsync desde IBaseRepository
        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            var sql = "SELECT * FROM Vehicles";
            return await _dbConnection.QueryAsync<Vehicle>(sql);
        }

        // Implementación de GetByIdAsync desde IBaseRepository
        public async Task<Vehicle> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Vehicles WHERE IdVehicle = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Vehicle>(sql, new { Id = id });
        }

        // Implementación de AddAsync desde IBaseRepository
        public async Task AddAsync(Vehicle entity)
        {
            var sql = "INSERT INTO Vehicles (IdClient, Brand, Model, Plate) VALUES (@IdClient, @Brand, @Model, @Plate)";
            await _dbConnection.ExecuteAsync(sql, entity);
        }

        // Implementación de Update desde IBaseRepository
        public void Update(Vehicle entity)
        {
            var sql = "UPDATE Vehicles SET IdClient = @IdClient, Brand = @Brand, Model = @Model, Plate = @Plate WHERE IdVehicle = @IdVehicle";
            _dbConnection.Execute(sql, entity);
        }

        // Implementación de DeleteAsync desde IBaseRepository
        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM Vehicles WHERE IdVehicle = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }

        // Implementación de GetByPlateAsync desde IVehicleRepository
        public async Task<Vehicle?> GetByPlateAsync(string plate)
        {
            var sql = "SELECT * FROM Vehicles WHERE Plate = @Plate LIMIT 1";
            return await _dbConnection.QueryFirstOrDefaultAsync<Vehicle>(sql, new { Plate = plate });
        }

        // Implementación de GetVehiclesByClientIdAsync desde IVehicleRepository
        public async Task<IEnumerable<Vehicle>> GetVehiclesByClientIdAsync(int clientId)
        {
            var sql = "SELECT * FROM Vehicles WHERE IdClient = @ClientId";
            return await _dbConnection.QueryAsync<Vehicle>(sql, new { ClientId = clientId });
        }

        // Implementación de GetVehiclesByFilterAsync desde IVehicleRepository
        public async Task<IReadOnlyList<Vehicle>> GetVehiclesByFilterAsync(VehicleQueryFilter filter)
        {
            var sql = "SELECT * FROM Vehicles WHERE 1=1";

            if (!string.IsNullOrEmpty(filter.Plate))
                sql += " AND Plate LIKE @Plate";

            if (!string.IsNullOrEmpty(filter.Brand))
                sql += " AND Brand LIKE @Brand";

            if (filter.ClientId.HasValue)
                sql += " AND IdClient = @ClientId";

            return (await _dbConnection.QueryAsync<Vehicle>(sql, filter)).ToList();
        }
    }
}
