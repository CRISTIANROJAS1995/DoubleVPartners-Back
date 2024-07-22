
using System.ComponentModel.DataAnnotations;

namespace Domain.Input
{
    public class UsuarioEliminarInput
    {
        [Required]
        public string Identificador { get; set; }
    }
}
