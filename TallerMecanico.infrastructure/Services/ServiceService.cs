// Infrastructure/Services/ServiceService.cs

using AutoMapper;
using TallerMecanico.Core.Dtos;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Core.Entities;
using TallerMecanico.Core.Exceptions;
using TallerMecanico.Core.QueryFilters;

namespace TallerMecanico.Infrastructure.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Obtener servicios por vehículo con paginación
        public async Task<IReadOnlyList<ServiceResponse>> GetByVehicleAsync(int idVehicle, PaginationQueryFilter pagination)
        {
            var services = await _unitOfWork.Services.GetServicesByVehicleAsync(idVehicle); // Usando Dapper para obtener los servicios por vehículo

            // Mapeando de Service a ServiceResponse usando AutoMapper
            var mappedServices = _mapper.Map<List<ServiceResponse>>(services);

            // Aplicando la paginación
            var pagedServices = PagedList<ServiceResponse>.Create(mappedServices, pagination.PageNumber, pagination.PageSize);

            return pagedServices;
        }

        // Crear un servicio
        public async Task<int> CreateAsync(CreateServiceRequest dto)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(dto.IdVehicle);
            if (vehicle is null)
                throw new BusinessException("IdVehicle no existe", "VEHICLE_NOT_FOUND", 404);

            if (!string.IsNullOrWhiteSpace(dto.Description) && dto.Description.Length > 200)
                throw new BusinessException("Descripción excede 200 caracteres", "DESCRIPTION_TOO_LONG", 400);

            if (dto.DateService.HasValue && dto.DateService.Value > DateTime.UtcNow.Date)
                throw new BusinessException("DateService no puede ser futura", "FUTURE_SERVICE_DATE", 400);

            var entity = _mapper.Map<WorkshopService>(dto);
            await _unitOfWork.Services.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity.IdService;
        }
    }
}
