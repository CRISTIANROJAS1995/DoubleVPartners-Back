using Domain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using Domain.Input;
using Domain.Dtos;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsuarioService _userService;

        public UserController(
            IUsuarioService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("Auth")]
        public async Task<IActionResult> Authenticate([FromBody] LoginInput request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userService.Authenticate(request));
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Add([FromBody] UsuarioInput request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userService.Add(request));
        }

        [Authorize]
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update([FromBody] UsuarioActualizarInput request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userService.Update(request, User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut]
        [Route("Admin/Update")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateToAdmin([FromBody] UsuarioActualizarAdminInput request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userService.UpdateToAdmin(request));
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete]
        [Route("Admin/Delete")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteToAdmin([FromBody] UsuarioEliminarInput request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userService.DeleteToAdmin(request.Identificador!));
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(UsuarioDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get()
        {
            var consult = await _userService.ByIdentifierDto(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (consult.Identificador == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        [Route("All")]
        [ProducesResponseType(200, Type = typeof(List<UsuarioDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _userService.All();
            return Ok(response);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UsuarioDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(string id)
        {
            var consult = await _userService.ByIdentifierDto(id);
            if (consult.Identificador == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }

    }
}
