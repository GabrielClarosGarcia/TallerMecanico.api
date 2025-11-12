using TallerMecanico.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TallerMecanico.Core.Interfaces
{
    public interface IServiceRepository : IBaseRepository<WorkshopService>
    {
        Task<IEnumerable<WorkshopService>> GetServicesByVehicleAsync(int vehicleId);
    }
}
