namespace TallerMecanico.Core.Dtos;

public record CreateVehicleRequest(int IdClient, string? Brand, string? Model, string? Plate);
public record VehicleResponse(int IdVehicle, int IdClient, string? Brand, string? Model, string? Plate);
