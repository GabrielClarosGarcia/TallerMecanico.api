using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerMecanico.Core.Dtos;
using TallerMecanico.Core.Entities;
using TallerMecanico.Infrastructure.Services;
using AutoMapper;
using TallerMecanico.Core.Enum;
using TallerMecanico.Core.Interfaces;

namespace TallerMecanico.Api.Controllers
{

    [Authorize(Roles = nameof(RoleType.Administrator))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;

        public SecurityController(ISecurityService securityService, IMapper mapper)
        {
            _securityService = securityService;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SecurityDto securityDto)
        {
 
            var security = _mapper.Map<Security>(securityDto);


            await _securityService.RegisterUser(security);


            securityDto = _mapper.Map<SecurityDto>(security);


            var response = new ApiResponse<SecurityDto>(true, securityDto, "Usuario registrado exitosamente.");
            return Ok(response);
        }

    }
}
