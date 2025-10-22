using TallerMecanico.Core.Dtos;

namespace TallerMecanico.Core.Interfaces;

public interface IServiceService
{
    Task<int> CreateAsync(CreateServiceRequest dto);
    Task<IReadOnlyList<ServiceResponse>> GetByVehicleAsync(int idVehicle);
}
