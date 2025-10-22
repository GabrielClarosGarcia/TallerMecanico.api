using AutoMapper;
using TallerMecanico.Core.Dtos;
using TallerMecanico.infrastructure.Entities;

namespace TallerMecanico.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        
        CreateMap<client, ClientResponse>();
        CreateMap<vehicle, VehicleResponse>();
        CreateMap<service, ServiceResponse>();

        
        CreateMap<CreateClientRequest, client>();
        CreateMap<CreateVehicleRequest, vehicle>();
        CreateMap<CreateServiceRequest, service>();
    }
}
