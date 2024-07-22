using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Input.Task
{
    public class TareaAsignarInput
    {
        [Required]
        public string IdentificadorUsuario { get; set; }

        [Required]
        public string IdentificadorTarea { get; set; }
    }
}
