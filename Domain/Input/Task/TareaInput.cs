using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Input.Task
{
    public class TareaInput
    {
        public int EstadoTareaId { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
    }
}
