// Core/Interfaces/IVehicleService.cs

using TallerMecanico.Core.Dtos;
using TallerMecanico.Core.QueryFilters;

namespace TallerMecanico.Core.Interfaces
{
    public interface IVehicleService
    {
        Task<int> CreateAsync(CreateVehicleRequest dto);
        Task<IReadOnlyList<VehicleResponse>> GetByClientAsync(int idClient, VehicleQueryFilter filter, PaginationQueryFilter filters);  // Agregar paginación
        Task<bool> PlateExistsAsync(string plate);
    }
}
