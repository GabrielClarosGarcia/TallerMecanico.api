using TallerMecanico.Core.Entities;
using TallerMecanico.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TallerMecanico.Core.Interfaces
{
    public interface IVehicleRepository : IBaseRepository<Vehicle>
    {
        Task<Vehicle?> GetByPlateAsync(string plate);
        Task<IEnumerable<Vehicle>> GetVehiclesByClientIdAsync(int clientId);
        Task<IReadOnlyList<Vehicle>> GetVehiclesByFilterAsync(VehicleQueryFilter filter); // Esta línea es importante
    }
}
