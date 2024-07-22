using Domain.Dtos;
using Domain.Input;
using Domain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController : ControllerBase
    {
        private readonly IGenericService _genericService;

        public GenericController(IGenericService genericService)
        {
            _genericService = genericService;
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        [Route("AllRole")]
        [ProducesResponseType(200, Type = typeof(List<RolDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AllRole()
        {
            var response = await _genericService.AllRole();
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        [Route("AllStateTask")]
        [ProducesResponseType(200, Type = typeof(List<EstadoTareaDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AllStateTask()
        {
            var response = await _genericService.AllStateTask();
            return Ok(response);
        }

    }
}
