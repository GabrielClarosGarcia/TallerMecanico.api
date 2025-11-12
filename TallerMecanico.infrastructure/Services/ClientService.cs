// Infrastructure/Services/ClientService.cs

using AutoMapper;
using TallerMecanico.Core.Dtos;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Core.Entities;
using TallerMecanico.Core.Exceptions;
using TallerMecanico.Core.QueryFilters;
using System.Linq;

namespace TallerMecanico.Infrastructure.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Crear un cliente
        public async Task<int> CreateAsync(CreateClientRequest dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new BusinessException("El nombre del cliente es obligatorio", "CLIENT_NAME_REQUIRED", 400);
            }

            var entity = _mapper.Map<Client>(dto);
            await _unitOfWork.Clients.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity.IdClient;
        }

        // Obtener un cliente por ID usando Dapper
        public async Task<ClientResponse?> GetAsync(int idClient)
        {
            var client = await _unitOfWork.Clients.GetByIdAsync(idClient); // Usando Dapper para obtener el cliente
            return client is null ? null : _mapper.Map<ClientResponse>(client);
        }

        // Obtener todos los clientes con paginación
        public async Task<IReadOnlyList<ClientResponse>> GetAllClientsAsync(PaginationQueryFilter filters)
        {
            // Usando Dapper para obtener todos los clientes
            var clients = await _unitOfWork.Clients.GetAllAsync();

            // Mapeando de Client a ClientResponse usando AutoMapper
            var mappedClients = _mapper.Map<List<ClientResponse>>(clients);

            // Aplicando la paginación
            var pagedClients = PagedList<ClientResponse>.Create(mappedClients, filters.PageNumber, filters.PageSize);

            return pagedClients;
        }

        // Buscar clientes por nombre usando Dapper
        public async Task<IReadOnlyList<ClientResponse>> SearchAsync(string? q, PaginationQueryFilter filters)
        {
            var clients = await _unitOfWork.Clients.GetByNameAsync(q);  // Usando Dapper para la búsqueda

            // Mapeando de Client a ClientResponse usando AutoMapper
            var mappedClients = _mapper.Map<List<ClientResponse>>(clients);

            // Aplicando la paginación
            var pagedClients = PagedList<ClientResponse>.Create(mappedClients, filters.PageNumber, filters.PageSize);

            return pagedClients;
        }

        // Actualizar un cliente
        public async Task<bool> UpdateAsync(UpdateClientRequest dto)
        {
            var client = await _unitOfWork.Clients.GetByIdAsync(dto.IdClient);
            if (client is null) return false;

            client.Name = dto.Name;
            _unitOfWork.Clients.Update(client);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        // Eliminar un cliente
        public async Task<bool> DeleteAsync(int idClient)
        {
            await _unitOfWork.Clients.DeleteAsync(idClient);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
