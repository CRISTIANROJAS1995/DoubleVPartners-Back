
using System.ComponentModel.DataAnnotations;

namespace Domain.Input.User
{
    public class UsuarioEliminarInput
    {
        [Required]
        public string Identificador { get; set; }
    }
}
