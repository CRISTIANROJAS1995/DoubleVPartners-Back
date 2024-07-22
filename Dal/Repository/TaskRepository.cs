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
            return await _context.Usuario.AsNoTracking().FirstOrDefaultAsync(c => c.Identificador == identifier);
        }


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
