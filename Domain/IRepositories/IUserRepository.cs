using Domain.Dtos;
using Domain.Entities;
using Domain.Input;

namespace Domain.IRepositories
{
    public interface IUsuarioRepository
    {
        Task<int?> Add(Usuario model);
        Task<int?> Update(Usuario model);
        Task<int?> Delete(Usuario model);
        Task<List<UsuarioDto>?> All();
        Task<Usuario?> ByEmail(string email);
        Task<Usuario?> ByIdentification(string numberIdentification);
        Task<Usuario?> ByIdentifier(string identifier);
        Task<UsuarioDto> ByIdentifierDto(string identifier);
        Task<UsuarioDto> ValidAuth(LoginInput model);

    }
}
