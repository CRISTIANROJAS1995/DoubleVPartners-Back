using Domain.Dtos.Generic;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Domain.IRepositories;
using Domain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Input.User;
using Domain.Input.Task;

namespace Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<ResponseApiDto?> Add(TareaInput request, string identificadorUsuario)
        {
            var response = new ResponseApiDto();

            var task = await _taskRepository.ByName(request.Nombre);
            if (task == null)
            {
                var user = await _taskRepository.ByUserIdentifier(identificadorUsuario);
                if (user == null)
                {
                    response.Result = false;
                    response.Message = "El usuario no existe.";
                }
                else
                {
                    Guid obj = Guid.NewGuid();
                    Tarea model = new()
                    {
                        Identificador = obj.ToString(),
                        UsuarioId = user.Id,
                        EstadoTareaId = request.EstadoTareaId,
                        Nombre = request.Nombre,
                        Descripcion = request.Descripcion,
                        FechaCreacion = DateTime.Now,
                        FechaActualizacion = DateTime.Now
                    };

                    var responseAdd = await _taskRepository.Add(model);
                    if (responseAdd == 1)
                    {
                        response.Result = true;
                    }
                    else
                    {
                        response.Result = false;
                        response.Message = "Ocurrio un error inesperado al guardar la tarea.";
                    }
                }
            }
            else
            {
                response.Result = false;
                response.Message = "La tarea ya se encuentra registrada.";
            }

            return response;
        }

        public async Task<ResponseApiDto?> Update(TareaActualizarInput request)
        {
            var response = new ResponseApiDto();

            var task = await _taskRepository.ByIdentifier(request.Identificador);
            if (task == null)
            {
                response.Result = false;
                response.Message = "La tarea no existe.";
            }
            else
            {
                task.EstadoTareaId = request.EstadoTareaId > 0 ? request.EstadoTareaId : task.EstadoTareaId;
                task.Nombre = request.Nombre != null ? request.Nombre : task.Nombre;
                task.Descripcion = request.Descripcion != null ? request.Descripcion : task.Descripcion;
                task.FechaActualizacion = DateTime.Now;

                var responseUpdate = await _taskRepository.Update(task);
                if (responseUpdate == 1)
                {
                    response.Result = true;
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado al actualizar la tarea.";
                }
            }

            return response;
        }

        public async Task<ResponseApiDto?> Delete(string identificador)
        {
            var response = new ResponseApiDto();

            var task = await _taskRepository.ByIdentifier(identificador);
            if (task == null)
            {
                response.Result = false;
                response.Message = "La tarea no existe.";
            }
            else
            {
                var responseDelete = await _taskRepository.Delete(task);
                if (responseDelete == 1)
                {
                    response.Result = true;
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado al eliminar la tarea.";
                }
            }

            return response;
        }

        public async Task<List<TareaDto>?> All()
        {
            return await _taskRepository.All();
        }

        public async Task<Tarea?> ByIdentifier(string identifier)
        {
            return await _taskRepository.ByIdentifier(identifier);
        }

        public async Task<ResponseApiDto?> ByName(string name)
        {
            var response = new ResponseApiDto();

            var user = await _taskRepository.ByName(name);
            if (user != null)
            {
                response.Result = true;
            }

            return response;
        }


        #region Asignación de tareas

        public async Task<ResponseApiDto?> AddAssignTask(TareaAsignarInput request, string usuarioAsignador)
        {
            var response = new ResponseApiDto();

            var valid = await _taskRepository.ValidAssignTask(request.IdentificadorUsuario, request.IdentificadorTarea);
            if (valid == null)
            {
                var user = await _taskRepository.ByUserIdentifier(request.IdentificadorUsuario);
                if (user == null)
                {
                    response.Result = false;
                    response.Message = "El usuario no existe.";
                }
                else
                {
                    var task = await _taskRepository.ByIdentifier(request.IdentificadorTarea);
                    if (task == null)
                    {
                        response.Result = false;
                        response.Message = "La tarea no existe.";
                    }
                    else
                    {
                        AsignacionTarea model = new()
                        {
                            UsuarioId = user.Id,
                            TareaId = task.Id,
                            FechaAsignacion = DateTime.Now,
                            UsuarioAsignador = usuarioAsignador
                        };

                        var responseAdd = await _taskRepository.AddAssignTask(model);
                        if (responseAdd == 1)
                        {
                            response.Result = true;
                        }
                        else
                        {
                            response.Result = false;
                            response.Message = "Ocurrio un error inesperado al asignar la tarea.";
                        }
                    }
                }
            }
            else
            {
                response.Result = false;
                response.Message = "Esta tarea ya fue asignada anteriormente al usuario.";
            }

            return response;
        }

        public async Task<List<AsignacionTareaDto>?> AllAssignTask()
        {
            return await _taskRepository.AllAssignTask();
        }

        public async Task<List<AsignacionTareaDto>?> ByUserAssignTask(string identificador)
        {
            List<AsignacionTareaDto> response = new List<AsignacionTareaDto>();

            var user = await _taskRepository.ByUserIdentifier(identificador);
            if (user != null)
            {
                var consultData = await _taskRepository.ByUserAssignTask(user.Id);
                if (consultData != null)
                {
                    response.AddRange(consultData);
                }
            }

            return response;
        }

        #endregion
    }
}
