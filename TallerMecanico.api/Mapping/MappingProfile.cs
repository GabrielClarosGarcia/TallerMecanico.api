using AutoMapper;
using TallerMecanico.Core.Dtos;
using TallerMecanico.Core.Entities;

namespace TallerMecanico.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<Client, ClientResponse>();
        CreateMap<Vehicle, VehicleResponse>();
        CreateMap<WorkshopService, ServiceResponse>();


        CreateMap<CreateClientRequest, Client>();
        CreateMap<CreateVehicleRequest, Vehicle>();
        CreateMap<CreateServiceRequest, WorkshopService>();
    }
}

