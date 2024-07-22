using Domain.Dtos;
using Domain.Entities;
using Domain.IRepositories;
using Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Web;
using System;
using Domain.Input;

namespace Dal.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioRepository(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int?> Add(Usuario model)
        {
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> Update(Usuario model)
        {
            _context.Update(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<UsuarioDto>?> All()
        {
            List<UsuarioDto> response = new List<UsuarioDto>();

            var list = await _context.Usuario
                 .AsNoTracking()
                 .Include("Rol")
                 .OrderBy(c => c.FechaCreacion)
                 .ToListAsync();

            if (list.Count > 0)
            {
                response.AddRange(list.AsEnumerable().Select(g => UsuarioDto(g)).ToList()!);
            }

            return response;
        }

        public async Task<Usuario?> ByEmail(string email)
        {
            return await _context.Usuario.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Usuario?> ByIdentification(string numberIdentification)
        {
            return await _context.Usuario.AsNoTracking().FirstOrDefaultAsync(c => c.Identificador == numberIdentification);
        }

        public async Task<Usuario?> ByIdentifier(string identifier)
        {
            return await _context.Usuario.AsNoTracking().FirstOrDefaultAsync(c => c.Identificador == identifier);
        }

        public async Task<UsuarioDto> ByIdentifierDto(string identifier)
        {
            var obj = await _context.Usuario
                 .AsNoTracking()
                 .Include("Rol")
                 .FirstOrDefaultAsync(c => c.Identificador == identifier);

            return UsuarioDto(obj);
        }

        public async Task<UsuarioDto> ValidAuth(LoginInput model)
        {
            var consult = await _context.Usuario
                 .AsNoTracking()
                 .Include("Rol")
                 .FirstOrDefaultAsync((u => u.Email == model.Email && u.Password == model.Password));

            return UsuarioDto(consult);
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
