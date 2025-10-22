using TallerMecanico.Core.Dtos;

namespace TallerMecanico.Core.Interfaces;

public interface IClientService
{
    Task<int> CreateAsync(CreateClientRequest dto);
    Task<ClientResponse?> GetAsync(int idClient);
    Task<IReadOnlyList<ClientResponse>> SearchAsync(string? q);
    Task<bool> UpdateAsync(UpdateClientRequest dto);
    Task<bool> DeleteAsync(int idClient);
}
