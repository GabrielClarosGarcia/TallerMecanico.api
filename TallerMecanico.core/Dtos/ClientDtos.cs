namespace TallerMecanico.Core.Dtos;

public record CreateClientRequest(string Name);
public record UpdateClientRequest(int IdClient, string Name);
public record ClientResponse(int IdClient, string Name);
