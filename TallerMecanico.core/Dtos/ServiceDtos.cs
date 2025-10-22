namespace TallerMecanico.Core.Dtos;

// Usa DateOnly? porque tu columna DateService es DATE en MySQL (Pomelo => DateOnly)
public record CreateServiceRequest(int IdVehicle, string? Description, DateOnly? DateService);
public record ServiceResponse(int IdService, int IdVehicle, string? Description, DateOnly? DateService);
