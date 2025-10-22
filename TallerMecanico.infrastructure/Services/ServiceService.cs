using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TallerMecanico.Core.Dtos;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.infrastructure.Data;
using TallerMecanico.infrastructure.Entities;

namespace TallerMecanico.Infrastructure.Services;

public class ServiceService : IServiceService
{
    private readonly WorkshopContext _db;
    private readonly IMapper _mapper;

    public ServiceService(WorkshopContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<int> CreateAsync(CreateServiceRequest dto)
    {
        if (!await _db.vehicles.AnyAsync(v => v.IdVehicle == dto.IdVehicle))
            throw new KeyNotFoundException("IdVehicle no existe");

        if (!string.IsNullOrWhiteSpace(dto.Description) && dto.Description.Length > 200)
            throw new InvalidOperationException("Description excede 200 caracteres");

        if (dto.DateService.HasValue &&
            dto.DateService.Value > DateOnly.FromDateTime(DateTime.UtcNow.Date))
            throw new InvalidOperationException("DateService no puede ser futura");

        var entity = _mapper.Map<service>(dto);
        _db.services.Add(entity);
        await _db.SaveChangesAsync();
        return entity.IdService;
    }

    public async Task<IReadOnlyList<ServiceResponse>> GetByVehicleAsync(int idVehicle)
    {
        var list = await _db.services
            .Where(s => s.IdVehicle == idVehicle)
            .ToListAsync();

        return _mapper.Map<List<ServiceResponse>>(list);
    }

    // (Opcional, si agregaste el historial con filtros)
    public async Task<IReadOnlyList<ServiceResponse>> GetHistoryAsync(int? idClient, int? idVehicle, DateOnly? from, DateOnly? to)
    {
        var q = _db.services.AsQueryable();

        if (idVehicle.HasValue)
            q = q.Where(s => s.IdVehicle == idVehicle.Value);

        if (idClient.HasValue)
            q = from s in q
                join v in _db.vehicles on s.IdVehicle equals v.IdVehicle
                where v.IdClient == idClient.Value
                select s;

        if (from.HasValue)
            q = q.Where(s => s.DateService != null && s.DateService.Value >= from.Value);

        if (to.HasValue)
            q = q.Where(s => s.DateService != null && s.DateService.Value <= to.Value);

        var list = await q
            .OrderByDescending(s => s.DateService)
            .ToListAsync();

        return _mapper.Map<List<ServiceResponse>>(list);
    }
}
