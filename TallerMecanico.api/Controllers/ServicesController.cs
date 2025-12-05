// API/Controllers/ServicesController.cs

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
    [Route("api/v{version:apiVersion}/services")]
    //[Route("api/v1/services")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _svc;
        public ServicesController(IServiceService svc) => _svc = svc;

        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] CreateServiceRequest dto)
        {
            try
            {
                var id = await _svc.CreateAsync(dto);
                return CreatedAtAction(nameof(GetByVehicle), new { idVehicle = dto.IdVehicle }, new ApiResponse<int>(true, id, "Servicio creado"));
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

        [HttpGet("by-vehicle/{idVehicle:int}")]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<ServiceResponse>>>> GetByVehicle(int idVehicle, [FromQuery] PaginationQueryFilter pagination)
        {
            var data = await _svc.GetByVehicleAsync(idVehicle, pagination);
            return Ok(new ApiResponse<IReadOnlyList<ServiceResponse>>(true, data)
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
    }
}
