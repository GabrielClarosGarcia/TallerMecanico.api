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
        CreateMap<Service, ServiceResponse>();


        CreateMap<CreateClientRequest, Client>();
        CreateMap<CreateVehicleRequest, Vehicle>();
        CreateMap<CreateServiceRequest, Service>();
        CreateMap<Security, SecurityDto>().ReverseMap();
    }
}

