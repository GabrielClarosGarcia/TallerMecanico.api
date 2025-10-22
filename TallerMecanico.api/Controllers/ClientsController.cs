using Microsoft.AspNetCore.Mvc;
using TallerMecanico.Core;
using TallerMecanico.Core.Dtos;
using TallerMecanico.Core.Interfaces;

namespace TallerMecanico.Api.Controllers;

[ApiController]
[Route("api/v1/clients")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _svc;
    public ClientsController(IClientService svc) => _svc = svc;

    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] CreateClientRequest dto)
    {
        var id = await _svc.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { idClient = id }, new ApiResponse<int>(true, id));
    }

    [HttpGet("{idClient:int}")]
    public async Task<ActionResult<ApiResponse<ClientResponse>>> Get(int idClient)
    {
        var data = await _svc.GetAsync(idClient);
        return data is null
            ? NotFound(new ApiResponse<object>(false, null, "No encontrado", "NOT_FOUND"))
            : Ok(new ApiResponse<ClientResponse>(true, data));
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<ClientResponse>>>> Search([FromQuery] string? q)
    {
        var data = await _svc.SearchAsync(q);
        return Ok(new ApiResponse<IReadOnlyList<ClientResponse>>(true, data));
    }

    [HttpPut("{idClient:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(int idClient, [FromBody] UpdateClientRequest dto)
    {
        if (dto.IdClient != idClient)
            return BadRequest(new ApiResponse<object>(false, null, "Ids no coinciden", "BAD_REQUEST"));
        var ok = await _svc.UpdateAsync(dto);
        return ok
            ? Ok(new ApiResponse<bool>(true, true))
            : NotFound(new ApiResponse<object>(false, null, "No encontrado", "NOT_FOUND"));
    }

    [HttpDelete("{idClient:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int idClient)
    {
        var ok = await _svc.DeleteAsync(idClient);
        return ok
            ? Ok(new ApiResponse<bool>(true, true))
            : NotFound(new ApiResponse<object>(false, null, "No encontrado", "NOT_FOUND"));
    }
}
