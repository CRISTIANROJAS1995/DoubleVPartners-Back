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
    public class GenericRepository : IGenericRepository
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(
            ApplicationDbContext context
            )
        {
            _context = context;
        }

        public async Task<List<RolDto>?> AllRole()
        {
            List<RolDto> response = new List<RolDto>();

            var list = await _context.Rol
                 .AsNoTracking()
                 .ToListAsync();

            if (list.Count > 0)
            {
                response.AddRange(list.AsEnumerable().Select(g => RolDto(g)).ToList()!);
            }

            return response;
        }

        public async Task<List<EstadoTareaDto>?> AllStateTask()
        {
            List<EstadoTareaDto> response = new List<EstadoTareaDto>();

            var list = await _context.EstadoTarea
                 .AsNoTracking()
                 .ToListAsync();

            if (list.Count > 0)
            {
                response.AddRange(list.AsEnumerable().Select(g => EstadoTareaDto(g)).ToList()!);
            }

            return response;
        }

        private RolDto RolDto(Rol? consult)
        {
            RolDto response = new RolDto();

            if (consult != null)
            {
                RolDto objRole = new RolDto();
                objRole.Id = consult.Id;
                objRole.Nombre = consult.Nombre;

                response.Id = consult.Id;
                response.Nombre = consult.Nombre;
            }

            return response;
        }

        private EstadoTareaDto EstadoTareaDto(EstadoTarea? consult)
        {
            EstadoTareaDto response = new EstadoTareaDto();

            if (consult != null)
            {
                EstadoTareaDto objEstadoTarea = new EstadoTareaDto();
                objEstadoTarea.Id = consult.Id;
                objEstadoTarea.Nombre = consult.Nombre;

                response.Id = consult.Id;
                response.Nombre = consult.Nombre;
            }

            return response;
        }

    }
}
