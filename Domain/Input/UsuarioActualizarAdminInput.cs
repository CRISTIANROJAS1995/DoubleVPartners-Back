using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Input
{
    public class UsuarioActualizarAdminInput
    {
        public string Identificador { get; set; }

        public int RolId { get; set; }

        public string? Password { get; set; }

        public string? Nombre { get; set; }

        public string? NumeroIdentificacion { get; set; }

        public string? Telefono { get; set; }
    }
}
