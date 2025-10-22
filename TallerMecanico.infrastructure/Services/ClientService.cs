using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TallerMecanico.Core.Dtos;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.infrastructure.Data;
using TallerMecanico.infrastructure.Entities;
namespace TallerMecanico.Infrastructure.Services;

public class ClientService : IClientService
{
    private readonly WorkshopContext _db;
    private readonly IMapper _mapper;

    public ClientService(WorkshopContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<int> CreateAsync(CreateClientRequest dto)
    {
        var entity = _mapper.Map<client>(dto);
        _db.clients.Add(entity);
        await _db.SaveChangesAsync();
        return entity.IdClient;
    }

    public async Task<ClientResponse?> GetAsync(int idClient)
    {
        var c = await _db.clients.FindAsync(idClient);
        return c is null ? null : _mapper.Map<ClientResponse>(c);
    }

    public async Task<IReadOnlyList<ClientResponse>> SearchAsync(string? q)
    {
        var query = _db.clients.AsQueryable();
        if (!string.IsNullOrWhiteSpace(q))
            query = query.Where(c => c.Name.Contains(q));

        var list = await query.OrderBy(c => c.Name).ToListAsync();
        return _mapper.Map<List<ClientResponse>>(list);
    }

    public async Task<bool> UpdateAsync(UpdateClientRequest dto)
    {
        var c = await _db.clients.FindAsync(dto.IdClient);
        if (c is null) return false;

        c.Name = dto.Name;

        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int idClient)
    {
        var c = await _db.clients.FindAsync(idClient);
        if (c is null) return false;

        _db.clients.Remove(c);
        return await _db.SaveChangesAsync() > 0;
    }
}
