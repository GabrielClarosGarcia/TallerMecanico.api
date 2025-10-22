using Microsoft.AspNetCore.Mvc;
using TallerMecanico.Core;
using TallerMecanico.Core.Dtos;
using TallerMecanico.Core.Interfaces;

namespace TallerMecanico.Api.Controllers;

[ApiController]
[Route("api/v1/vehicles")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleService _svc;
    public VehiclesController(IVehicleService svc) => _svc = svc;

    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] CreateVehicleRequest dto)
    {
        try
        {
            var id = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByClient), new { idClient = dto.IdClient }, new ApiResponse<int>(true, id));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse<object>(false, null, ex.Message, "NOT_FOUND"));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponse<object>(false, null, ex.Message, "BUSINESS_VALIDATION"));
        }
    }

    [HttpGet("by-client/{idClient:int}")]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<VehicleResponse>>>> GetByClient(int idClient)
    {
        var data = await _svc.GetByClientAsync(idClient);
        return Ok(new ApiResponse<IReadOnlyList<VehicleResponse>>(true, data));
    }

    [HttpGet("plate-exists")]
    public async Task<ActionResult<ApiResponse<bool>>> PlateExists([FromQuery] string plate)
    {
        if (string.IsNullOrWhiteSpace(plate))
            return BadRequest(new ApiResponse<object>(false, null, "Debe enviar 'plate'", "BAD_REQUEST"));
        var exists = await _svc.PlateExistsAsync(plate);
        return Ok(new ApiResponse<bool>(true, exists));
    }
}
