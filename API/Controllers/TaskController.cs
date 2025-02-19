﻿using Domain.Dtos;
using Domain.Input.User;
using Domain.Input;
using Domain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Domain.Input.Task;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Add([FromBody] TareaInput request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _taskService.Add(request, User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        [Authorize]
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update([FromBody] TareaActualizarInput request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _taskService.Update(request));
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete([FromBody] UsuarioEliminarInput request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _taskService.Delete(request.Identificador!));
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        [Route("All")]
        [ProducesResponseType(200, Type = typeof(List<TareaDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _taskService.All();
            return Ok(response);
        }


        [Authorize(Roles = "Supervisor")]
        [HttpPost]
        [Route("Assign/Add")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddAssignTask([FromBody] TareaAsignarInput request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _taskService.AddAssignTask(request, User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        [Authorize(Roles = "Supervisor")]
        [HttpGet]
        [Route("Assign/All")]
        [ProducesResponseType(200, Type = typeof(List<AsignacionTareaDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AllAssignTask()
        {
            var response = await _taskService.AllAssignTask();
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        [Route("Assign/Get")]
        [ProducesResponseType(200, Type = typeof(List<AsignacionTareaDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get()
        {
            var consult = await _taskService.ByUserAssignTask(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (consult == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }

    }
}
