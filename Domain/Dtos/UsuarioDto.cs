
namespace Domain.Dtos
{
    public class UsuarioDto
    {
        public string? Identificador { get; set; }

        public RolDto Role { get; set; }

        public string? Email { get; set; }

        public string? Nombre { get; set; }

        public string? NumeroIdentificacion { get; set; }

        public string? Telefono { get; set; }
    }
}
