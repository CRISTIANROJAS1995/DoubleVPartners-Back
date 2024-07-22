using Domain.Dtos;
using Domain.Entities;
using Domain.Input;
using Domain.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TaskRepository(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int?> Add(Tarea model)
        {
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> Update(Tarea model)
        {
            _context.Update(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> Delete(Tarea model)
        {
            _context.Remove(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<TareaDto>?> All()
        {
            List<TareaDto> response = new List<TareaDto>();

            var list = await _context.Tarea
                 .AsNoTracking()
                 .Include("Usuario")
                 .Include("Usuario.Rol")
                 .Include("EstadoTarea")
                 .OrderBy(c => c.FechaCreacion)
                 .ToListAsync();

            if (list.Count > 0)
            {
                response.AddRange(list.AsEnumerable().Select(g => TareaDto(g)).ToList()!);
            }

            return response;
        }

        public async Task<Tarea?> ByName(string name)
        {
            return await _context.Tarea.AsNoTracking().FirstOrDefaultAsync(c => c.Nombre == name);
        }

        public async Task<Tarea?> ByIdentifier(string identifier)
        {
            return await _context.Tarea.AsNoTracking().FirstOrDefaultAsync(c => c.Identificador == identifier);
        }

        public async Task<Usuario?> ByUserIdentifier(string identifier)
        {
            return await _context.Usuario
                .AsNoTracking()
                .Include("Rol")
                .FirstOrDefaultAsync(c => c.Identificador == identifier);
        }


        #region Asignación de tareas

        public async Task<int?> AddAssignTask(AsignacionTarea model)
        {
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<AsignacionTarea?> ValidAssignTask(string identifierUser, string identifierTask)
        {
            return await _context.AsignacionTarea
              .AsNoTracking()
              .Include("Usuario")
              .Include("Usuario.Rol")
              .Include("Tarea")
              .Include("Tarea.EstadoTarea")
              .FirstOrDefaultAsync(c => c.Usuario.Identificador == identifierUser && c.Tarea.Identificador == identifierTask);
        }

        public async Task<List<AsignacionTareaDto>?> AllAssignTask()
        {
            List<AsignacionTareaDto> response = new List<AsignacionTareaDto>();

            var list = await _context.AsignacionTarea
                 .AsNoTracking()
                 .Include("Usuario")
                 .Include("Usuario.Rol")
                 .Include("Tarea")
                 .Include("Tarea.EstadoTarea")
                 .ToListAsync();

            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    response.Add(await AsignacionTareaDtoAsync(list[i]));
                }
            }

            return response;
        }

        public async Task<List<AsignacionTareaDto>?> ByUserAssignTask(int userId)
        {
            List<AsignacionTareaDto> response = new List<AsignacionTareaDto>();

            var list = await _context.AsignacionTarea
                 .AsNoTracking()
                 .Include("Usuario")
                 .Include("Usuario.Rol")
                 .Include("Tarea")
                 .Include("Tarea.EstadoTarea")
                 .Where(r => r.UsuarioId == userId)
                 .ToListAsync();

            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    response.Add(await AsignacionTareaDtoAsync(list[i]));
                }
            }

            return response;
        }

        private async Task<AsignacionTareaDto> AsignacionTareaDtoAsync(AsignacionTarea? consult)
        {
            AsignacionTareaDto response = new AsignacionTareaDto();

            if (consult != null)
            {
                UsuarioDto objUser = new UsuarioDto();
                objUser = UsuarioDto(consult.Usuario);

                TareaDto objTask = new TareaDto();
                objTask = TareaDto(consult.Tarea);

                UsuarioDto objUserCreated = new UsuarioDto();

                var consultUserCreated = await ByUserIdentifier(consult.UsuarioAsignador);
                if (consultUserCreated != null)
                {
                    objUserCreated = UsuarioDto(consultUserCreated);
                }

                response.Usuario = objUser;
                response.Tarea = objTask;
                response.FechaAsignacion = consult.FechaAsignacion;
                response.UsuarioAsignador = objUserCreated ?? null;
            }

            return response;
        }

        #endregion

        private TareaDto TareaDto(Tarea? consult)
        {
            TareaDto response = new TareaDto();

            if (consult != null)
            {
                UsuarioDto objUser = new UsuarioDto();
                objUser = UsuarioDto(consult.Usuario);

                EstadoTareaDto objState = new EstadoTareaDto() { 
                    Id = consult.EstadoTarea.Id,
                    Nombre = consult.EstadoTarea.Nombre
                };

                response.Identificador = consult.Identificador;
                response.Usuario = objUser;
                response.EstadoTarea = objState;
                response.Nombre = consult.Nombre;
                response.Descripcion = consult.Descripcion;
            }

            return response;
        }

        private UsuarioDto UsuarioDto(Usuario? consult)
        {
            UsuarioDto response = new UsuarioDto();

            if (consult != null)
            {
                RolDto objRole = new RolDto();
                objRole.Id = consult.Rol.Id;
                objRole.Nombre = consult.Rol.Nombre;

                response.Identificador = consult.Identificador;
                response.Role = objRole;
                response.Email = consult.Email;
                response.Nombre = consult.Nombre;
                response.NumeroIdentificacion = consult.NumeroIdentificacion;
                response.Telefono = consult.Telefono;
            }

            return response;
        }

    }
}
