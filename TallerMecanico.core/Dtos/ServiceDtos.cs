namespace TallerMecanico.Core.Dtos
{
    public record CreateServiceRequest(int IdVehicle, string? Description, DateTime? DateService);

    public record ServiceResponse(int IdService, int IdVehicle, string? Description, DateTime? DateService);
}
