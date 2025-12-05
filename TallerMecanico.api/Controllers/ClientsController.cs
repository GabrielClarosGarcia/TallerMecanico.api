// API/Controllers/ClientsController.cs

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerMecanico.Core.Dtos;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Core.QueryFilters;

namespace TallerMecanico.Api.Controllers
{
    /// <summary>
    /// Controlador para gestionar los clientes del taller.
    /// </summary>
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/clients")]
    //[Route("api/v1/clients")]
    public class ClientsController : ControllerBase 
    {
        private readonly IClientService _svc;
        public ClientsController(IClientService svc) => _svc = svc;

        /// <summary>
        /// Crea un nuevo cliente.
        /// </summary>
        /// <param name="dto">Objeto con la información del cliente.</param>
        /// <returns>El ID del cliente creado.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] CreateClientRequest dto)
        {
            var id = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { idClient = id }, new ApiResponse<int>(true, id, "Cliente creado exitosamente"));
        }

        /// <summary>
        /// Obtiene los detalles de un cliente por su ID.
        /// </summary>
        /// <param name="idClient">ID del cliente.</param>
        /// <returns>El cliente con el ID proporcionado.</returns>
        [HttpGet("{idClient:int}")]
        public async Task<ActionResult<ApiResponse<ClientResponse>>> Get(int idClient)
        {
            var data = await _svc.GetAsync(idClient);
            return data is null
                ? NotFound(new ApiResponse<object>(false, null, "No encontrado", "NOT_FOUND"))
                : Ok(new ApiResponse<ClientResponse>(true, data, "Cliente encontrado"));
        }

        /// <summary>
        /// Busca clientes por nombre con paginación.
        /// </summary>
        /// <param name="q">Nombre del cliente a buscar.</param>
        /// <param name="filters">Filtros de paginación.</param>
        /// <returns>Lista de clientes encontrados.</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<ClientResponse>>>> Search([FromQuery] string? q, [FromQuery] PaginationQueryFilter filters)
        {
            var data = await _svc.SearchAsync(q, filters);
            return Ok(new ApiResponse<IReadOnlyList<ClientResponse>>(true, data, messages: new List<Message>
            {
                new Message { Type = "Success", Description = "Clientes obtenidos exitosamente." }
            })
            {
                Pagination = new Pagination
                {
                    TotalCount = data.Count(),
                    PageSize = filters.PageSize,
                    CurrentPage = filters.PageNumber,
                    TotalPages = (int)Math.Ceiling(data.Count() / (double)filters.PageSize)
                }
            });
        }

        /// <summary>
        /// Actualiza la información de un cliente.
        /// </summary>
        /// <param name="idClient">ID del cliente a actualizar.</param>
        /// <param name="dto">Datos del cliente actualizados.</param>
        /// <returns>Si la actualización fue exitosa.</returns>
        [HttpPut("{idClient:int}")]
        public async Task<ActionResult<ApiResponse<bool>>> Update(int idClient, [FromBody] UpdateClientRequest dto)
        {
            if (dto.IdClient != idClient)
                return BadRequest(new ApiResponse<object>(false, null, "Ids no coinciden", "BAD_REQUEST"));
            var ok = await _svc.UpdateAsync(dto);
            return ok
                ? Ok(new ApiResponse<bool>(true, true, "Cliente actualizado"))
                : NotFound(new ApiResponse<object>(false, null, "No encontrado", "NOT_FOUND"));
        }

        /// <summary>
        /// Elimina un cliente por su ID.
        /// </summary>
        /// <param name="idClient">ID del cliente a eliminar.</param>
        /// <returns>Si la eliminación fue exitosa.</returns>
        [HttpDelete("{idClient:int}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int idClient)
        {
            var ok = await _svc.DeleteAsync(idClient);
            return ok
                ? Ok(new ApiResponse<bool>(true, true, "Cliente eliminado"))
                : NotFound(new ApiResponse<object>(false, null, "No encontrado", "NOT_FOUND"));
        }
    }
}
