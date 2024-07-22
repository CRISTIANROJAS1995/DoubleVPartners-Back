using Domain.Dtos;
using Domain.Dtos.Generic;
using Domain.Entities;
using Domain.Extension;
using Domain.Input;
using Domain.Input.User;

namespace Domain.IServices
{
    public interface IUsuarioService
    {
        Task<ResponseApiDto> Authenticate(LoginInput request);
        Task<ResponseApiDto?> Add(UsuarioInput request);
        Task<ResponseApiDto?> Update(UsuarioActualizarInput request, string identifier);
        Task<ResponseApiDto?> UpdateToAdmin(UsuarioActualizarAdminInput request);
        Task<ResponseApiDto?> DeleteToAdmin(string identificador);
        Task<List<UsuarioDto>?> All();
        Task<UsuarioDto> ByIdentifierDto(string identifier);
        Task<Usuario?> ByIdentifier(string identifier);
        Task<ResponseApiDto?> ByEmail(string email);
        Task<ResponseApiDto?> ByIdentification(string numberIdentification);
    }
}
