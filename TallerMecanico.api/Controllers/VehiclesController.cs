// API/Controllers/VehiclesController.cs

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerMecanico.Core.Dtos;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Core.QueryFilters;

namespace TallerMecanico.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/vehicles")]
    //[Route("api/v1/vehicles")]
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
                return CreatedAtAction(nameof(GetByClient), new { idClient = dto.IdClient }, new ApiResponse<int>(true, id, "Vehículo creado"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse<object>(false, null, ex.Message, "NOT_FOUND"));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<object>(false, null, ex.Message, "BUSINESS_VALIDATION"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(false, null, ex.Message, "UNHANDLED"));
            }
        }

        [HttpGet("by-client/{idClient:int}")]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<VehicleResponse>>>> GetByClient(int idClient, [FromQuery] VehicleQueryFilter filter, [FromQuery] PaginationQueryFilter pagination)
        {
            filter.ClientId = idClient;
            var data = await _svc.GetByClientAsync(idClient, filter, pagination);
            return Ok(new ApiResponse<IReadOnlyList<VehicleResponse>>(true, data, messages: new List<Message>
            {
                new Message { Type = "Success", Description = "Vehículos obtenidos correctamente." }
            })
            {
                Pagination = new Pagination
                {
                    TotalCount = data.Count(),
                    PageSize = pagination.PageSize,
                    CurrentPage = pagination.PageNumber,
                    TotalPages = (int)Math.Ceiling(data.Count() / (double)pagination.PageSize)
                }
            });
        }

        [HttpGet("plate-exists")]
        public async Task<ActionResult<ApiResponse<bool>>> PlateExists([FromQuery] string plate)
        {
            if (string.IsNullOrWhiteSpace(plate))
                return BadRequest(new ApiResponse<object>(false, null, "Debe enviar 'plate'", "BAD_REQUEST"));
            var exists = await _svc.PlateExistsAsync(plate);
            return Ok(new ApiResponse<bool>(true, exists, "Placa verificada"));
        }
    }
}
