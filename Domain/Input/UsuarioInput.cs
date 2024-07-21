using System.ComponentModel.DataAnnotations;

namespace Domain.Input
{
    public class UsuarioInput
    {
        public int RolId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string NumeroIdentificacion { get; set; }

        public string Telefono { get; set; } = string.Empty;
    }
}
