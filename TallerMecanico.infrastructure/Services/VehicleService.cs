using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TallerMecanico.Core.Dtos;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.infrastructure.Data;
using TallerMecanico.infrastructure.Entities;


namespace TallerMecanico.Infrastructure.Services;

public class VehicleService : IVehicleService
{
    private readonly WorkshopContext _db;
    private readonly IMapper _mapper;

    public VehicleService(WorkshopContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<int> CreateAsync(CreateVehicleRequest dto)
    {
        // Validaciones de negocio
        if (!await _db.clients.AnyAsync(c => c.IdClient == dto.IdClient))
            throw new KeyNotFoundException("IdClient no existe");

        if (!string.IsNullOrWhiteSpace(dto.Plate) &&
            await _db.vehicles.AnyAsync(v => v.Plate == dto.Plate))
            throw new InvalidOperationException("Ya existe un vehículo con esa placa");

        var entity = _mapper.Map<vehicle>(dto);
        _db.vehicles.Add(entity);
        await _db.SaveChangesAsync();
        return entity.IdVehicle;
    }

    public async Task<IReadOnlyList<VehicleResponse>> GetByClientAsync(int idClient)
    {
        var list = await _db.vehicles
            .Where(v => v.IdClient == idClient)
            .ToListAsync();

        return _mapper.Map<List<VehicleResponse>>(list);
    }

    public async Task<bool> PlateExistsAsync(string plate)
        => await _db.vehicles.AnyAsync(v => v.Plate == plate);
}
