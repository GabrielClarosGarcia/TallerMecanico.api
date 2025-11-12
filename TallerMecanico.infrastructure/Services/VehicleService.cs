// Infrastructure/Services/VehicleService.cs

using AutoMapper;
using TallerMecanico.Core.Dtos;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Core.Entities;
using TallerMecanico.Core.QueryFilters;
using TallerMecanico.Core.Exceptions;

namespace TallerMecanico.Infrastructure.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VehicleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Obtener vehículos por cliente con paginación
        public async Task<IReadOnlyList<VehicleResponse>> GetByClientAsync(int idClient, VehicleQueryFilter filter, PaginationQueryFilter pagination)
        {
            filter.ClientId = idClient; // Asignar el idClient al filtro
            var vehicles = await _unitOfWork.Vehicles.GetVehiclesByFilterAsync(filter); // Usando Dapper para obtener los vehículos con los filtros

            // Mapeando de Vehicle a VehicleResponse usando AutoMapper
            var mappedVehicles = _mapper.Map<List<VehicleResponse>>(vehicles);

            // Aplicando la paginación
            var pagedVehicles = PagedList<VehicleResponse>.Create(mappedVehicles, pagination.PageNumber, pagination.PageSize);

            return pagedVehicles;
        }

        // Verificar si la placa de un vehículo ya existe
        public async Task<bool> PlateExistsAsync(string plate)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByPlateAsync(plate); // Usando Dapper para verificar si la placa ya existe
            return vehicle != null;
        }

        // Crear un vehículo
        public async Task<int> CreateAsync(CreateVehicleRequest dto)
        {
            var existsClient = await _unitOfWork.Clients.GetByIdAsync(dto.IdClient);
            if (existsClient is null)
                throw new BusinessException("IdClient no existe", 404);

            if (!string.IsNullOrWhiteSpace(dto.Plate))
            {
                var existingVehicle = await _unitOfWork.Vehicles.GetByPlateAsync(dto.Plate);
                if (existingVehicle != null)
                    throw new BusinessException("Ya existe un vehículo con esa placa", 400);
            }

            var entity = _mapper.Map<Vehicle>(dto);
            await _unitOfWork.Vehicles.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity.IdVehicle;
        }
    }
}
