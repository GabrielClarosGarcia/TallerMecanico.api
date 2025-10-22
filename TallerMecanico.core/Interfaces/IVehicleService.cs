using TallerMecanico.Core.Dtos;

namespace TallerMecanico.Core.Interfaces;

public interface IVehicleService
{
    Task<int> CreateAsync(CreateVehicleRequest dto);
    Task<IReadOnlyList<VehicleResponse>> GetByClientAsync(int idClient);
    Task<bool> PlateExistsAsync(string plate);
}
